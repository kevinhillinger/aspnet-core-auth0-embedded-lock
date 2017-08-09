namespace App.Security.AspNet.Authentication.Auth0
{
    public class LockContext
    {
        public string CallbackUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Domain { get; set; }
        public string Nonce { get; set; }
        public string State { get; set; }
    }
}