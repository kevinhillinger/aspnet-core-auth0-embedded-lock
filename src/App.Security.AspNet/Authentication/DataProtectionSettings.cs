namespace App.Security.AspNet.Authentication
{
    /// <summary>
    /// The settings to be used to configuration data protection
    /// </summary>
    public class DataProtectionSettings
    {
        public string StorageAccountConnectionString { get; set; }
        public bool UseDevelopmentStorageAccount { get; set; }

        public DataProtectionSettings()
        {
            UseDevelopmentStorageAccount = true; //default
        }
    }
}
