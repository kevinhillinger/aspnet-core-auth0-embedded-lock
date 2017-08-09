using Security.Authorization.Model;
using System;
using System.Collections.Generic;

namespace App.Security.Authorization.Data
{
    public interface IPermissionRepository
    {
        IEnumerable<Permission> All();

        /// <summary>
        /// Gets the effective set of permissions for the specific roles
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        ISet<Permission> GetPermissions(IEnumerable<Guid> roleIds);
    }
}
