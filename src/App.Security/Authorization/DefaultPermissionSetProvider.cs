using App.Security.Authorization.Data;
using System.Linq;
using App.Security.Authorization.Model;
using System.Collections.Immutable;
using System.Collections.Generic;

namespace App.Security.Authorization
{
    class DefaultPermissionSetProvider : IPermissionSetProvider
    {
        private readonly IPermissionRepository repository;
        private readonly IPermissionSetCache cache;
        private readonly IRoleRepository roleRepository;

        public DefaultPermissionSetProvider(IPermissionSetCache cache, IPermissionRepository repository, IRoleRepository roleRepository)
        {
            this.cache = cache;
            this.repository = repository;
            this.roleRepository = roleRepository;
        }

        public ImmutableHashSet<PermissionCode> GetEffectivePermissionSet(AuthorizationContextKey key)
        {
            if (!cache.Exists(key))
            {
                var roles = roleRepository.Get(key.TenantId, key.UserId);
                var permissions = repository.GetPermissions(roles.Select(r => r.Id));

                cache.Set(key, new HashSet<PermissionCode>(permissions.Select(p => p.Code)));

                return permissions.Select(p => p.Code).ToImmutableHashSet();
            }

            var codes = cache.Get(key);
            return codes;
        }

        public void Clear(AuthorizationContextKey key)
        {
            cache.Remove(key);
        }
    }
}
