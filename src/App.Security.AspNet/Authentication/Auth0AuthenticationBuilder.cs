using App.Security.AspNet.Authentication.Auth0;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;

namespace App.Security.AspNet.Authentication
{
    public sealed class Auth0AuthenticationBuilder
    {
        IConfigurationRoot configuration;
        IServiceCollection services;

        public Auth0AuthenticationBuilder(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        public Auth0AuthenticationBuilder Using(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //required by dependency chain in the authentication libraries

            this.services = services;

            return this;
        }
        

        public void AddAuth0()
        {
            services.AddOptions();

            ConfigureOpenIdConnectWithForAuth0();
            ConfigureDataProtection();
        }

        private void ConfigureOpenIdConnectWithForAuth0()
        {
            services.AddAuthentication(options => options.SignInScheme = Auth0Settings.AuthenticationScheme.Cookie);

            services.Configure<Auth0Settings>(configuration.GetSection(Auth0Settings.ConfigurationSectionName));
            var settings = configuration.GetSection(Auth0Settings.ConfigurationSectionName).Get<Auth0Settings>();

            services.Configure<OpenIdConnectOptions>(options =>
            {
                options.AuthenticationScheme = Auth0Settings.AuthenticationScheme.OpenIdConnect;

                options.Authority = $"https://{settings.Domain}";
                options.ClientId = settings.ClientId;
                options.ClientSecret = settings.ClientSecret;

                options.AutomaticAuthenticate = false;
                options.AutomaticChallenge = false;
                options.ResponseType = "code";
                options.CallbackPath = new PathString(settings.CallbackPath);
                options.ClaimsIssuer = Auth0Settings.AuthenticationScheme.OpenIdConnect; // Configure the Claims Issuer to be Auth0
            });
        }

        /// <summary>
        /// Adds data protection using blob storage
        /// </summary>
        /// <param name="services"></param>
        /// <remarks>
        /// The container must exist before calling the DataProtection APIs. 
        /// The specific file within the container does not have to exist, as it will be created on-demand.
        /// 
        /// Adding data protection will allow the app to be deployed on a cluster
        /// </remarks>
        private void ConfigureDataProtection()
        {
            services.Configure<DataProtectionSettings>(configuration.GetSection("DataProtection"));
            services.Configure<DataProtectionSettings>(settings => {

                var storageAccount = GetStorageAccount(settings);
                var client = storageAccount.CreateCloudBlobClient();
                var container = client.GetContainerReference("data-protection-keys");

                container.CreateIfNotExistsAsync().GetAwaiter().GetResult();
                services.AddDataProtection().PersistKeysToAzureBlobStorage(container, "keys.xml");
            });
        }

        private static CloudStorageAccount GetStorageAccount(DataProtectionSettings settings)
        {
            return settings.UseDevelopmentStorageAccount
                ? CloudStorageAccount.DevelopmentStorageAccount
                : CloudStorageAccount.Parse(settings.StorageAccountConnectionString);
        }
    }
}
