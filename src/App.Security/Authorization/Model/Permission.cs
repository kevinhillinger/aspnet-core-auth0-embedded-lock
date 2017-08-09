using App.Security.Authorization.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Authorization.Model
{
    [Serializable]
    public class Permission
    {
        public Guid Id { get; set; }

        public PermissionCode Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Permission()
        {
        }

        public Permission(PermissionCode code)
        {
            Code = code;
        }

        public bool Equals(Permission other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Code, Code) && Equals(other.Code, Code);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Permission)) return false;
            return Equals((Permission)obj);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}
