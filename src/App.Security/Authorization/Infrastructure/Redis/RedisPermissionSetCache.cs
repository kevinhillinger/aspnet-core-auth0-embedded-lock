
using App.Security.Authorization.Data;
using App.Security.Authorization.Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace App.Security.Authorization.Infrastructure.Redis
{
    internal sealed class RedisPermissionSetCache : IPermissionSetCache
    {
        IBinarySerializer serializer;
        IDatabase database;

        public RedisPermissionSetCache(IDatabase database, IBinarySerializer serializer)
        {
            this.database = database;
            this.serializer = serializer;
        }

        public bool Set(AuthorizationContextKey key, ISet<PermissionCode> value, TimeSpan? expiresIn = null)
        {
            var serializedValue = SerializeValue(value);
            string stringKey = key;

            return expiresIn.HasValue 
                ? database.StringSet(stringKey, serializedValue, expiresIn)
                : database.StringSet(stringKey, serializedValue);
        }

        public ImmutableHashSet<PermissionCode> Get(AuthorizationContextKey key)
        {
            string stringKey = key;
            var entry = database.StringGet(stringKey);

            if (entry.IsNull)
            {
                return new HashSet<PermissionCode>().ToImmutableHashSet();
            }

            return ((ISet<PermissionCode>)serializer.Deserialize(entry)).ToImmutableHashSet();
        }

        public bool Exists(AuthorizationContextKey key)
        {
            string stringKey = key;
            return database.KeyExists(stringKey);
        }

        public void Remove(AuthorizationContextKey key)
        {
            if (Exists(key))
            {
                string stringKey = key;
                database.KeyDelete(stringKey);
            }
        }

        private byte[] SerializeValue<T>(T value)
        {
            var entry = serializer.Serialize(value);
            return entry;
        }
    }
}
