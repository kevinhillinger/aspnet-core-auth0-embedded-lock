using Security.Authorization.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Security.Authorization.Configuration
{
    public sealed class AuthorizationConfigure
    {
        public IEnumerable<Permission> Permissions { get; set; }
    }
}
