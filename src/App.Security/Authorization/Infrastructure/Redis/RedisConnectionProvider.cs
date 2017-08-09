using StackExchange.Redis;
using System;

namespace Security.Authorization.Infrastructure.Redis
{
    public sealed class RedisConnectionProvider
    {
        /// <summary>
        /// Gets the connection name of the redis connection
        /// </summary>
        public static Func<string> ConnectionString = () => "";

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            var connectionString = ConnectionString();

            if (string.IsNullOrEmpty(connectionString))
                return null;
            return ConnectionMultiplexer.Connect(ConnectionString());
        });

        public ConnectionMultiplexer GetConnection()
        { 
            var connection = lazyConnection.Value;

            if (connection == null)
            {
                throw new InvalidOperationException("A Redis connection could not be established. Please check or set the connection string using RedisConnectionProvider.ConnectionString");
            }

            return connection;
        }
    }
}
