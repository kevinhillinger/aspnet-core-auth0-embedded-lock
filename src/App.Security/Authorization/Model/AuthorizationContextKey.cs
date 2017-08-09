
namespace App.Security.Authorization.Model
{
    /// <summary>
    /// Two things that must be used to authorization are: the user + tenant we're authorizing for. Permissions will be different.
    /// </summary>
    public struct AuthorizationContextKey
    {
        public string UserId { get; }
        public string TenantId { get; }

        private AuthorizationContextKey(string userId, string tenantId)
        {
            UserId = userId;
            TenantId = tenantId;
        }

        static public implicit operator string(AuthorizationContextKey key)
        {
            return string.Concat(key.TenantId, "/", key.UserId);
        }

        /// <summary>
        /// Factory for creating authz context keys.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public static AuthorizationContextKey Create(string userId, string tenantId)
        {
            GuardAgainstNullAndEmpty("userId", userId);
            GuardAgainstNullAndEmpty("tenantId", tenantId);

            return new AuthorizationContextKey(userId, tenantId);
        }

        public static AuthorizationContextKey Anonymous()
        {
            return new AuthorizationContextKey("anonymous", "anonymous");
        }

        //Thanking R# for the timesaver
        public bool Equals(AuthorizationContextKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.UserId, UserId) && Equals(other.TenantId, TenantId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AuthorizationContextKey)) return false;
            return Equals((AuthorizationContextKey)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = UserId != null ? UserId.GetHashCode() : 0;
                result = (result * 397) ^ (TenantId != null ? TenantId.GetHashCode() : 0);
                return result;
            }
        }

        public override string ToString()
        {
            return GetHashCode().ToString();
        }

        private static void GuardAgainstNullAndEmpty(string paramName, string value)
        {
            if (string.IsNullOrEmpty(value)) throw new System.ArgumentNullException(paramName, $"A null or empty {paramName} is not allowed for an AuthorizationContextKey");
        }
    }
}
