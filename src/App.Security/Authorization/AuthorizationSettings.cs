using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Security.Authorization
{
    public class AuthorizationSettings
    {
        public PermissionSetCacheSettings PermissionSetCache { get; set; }
    }

    public class PermissionSetCacheSettings
    {
        public string RedisConnectionString { get; set; }
    }
}
