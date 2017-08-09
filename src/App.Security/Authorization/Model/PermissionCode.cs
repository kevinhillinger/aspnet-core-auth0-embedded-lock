using System;

namespace App.Security.Authorization.Model
{
    [Serializable]
    public struct PermissionCode
    {
        public string Value { get; }

        public PermissionCode(string value)
        {
            GuardAgainstNullOrEmpty(value);
            Value = value;
        }

        static public implicit operator string(PermissionCode code)
        {
            return code.Value;
        }

        static public implicit operator PermissionCode(string value)
        {
            return new PermissionCode(value);
        }

        private static void GuardAgainstNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new InvalidOperationException("PermissionCode value cannot be null.");
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
