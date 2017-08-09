using App.Security.Authorization.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Security.Authorization.Data
{
    public interface IRoleRepository
    {
        IList<Role> Get(string tenantId, string userId);
    }
}
