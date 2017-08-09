using App.Security.Authorization;
using App.Security.Registration;
using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Security.Authorization.Infrastructure.Redis;

namespace App.Security.AspNet.Authorization
{
    public sealed class AspNetCoreAuthorizationBuilder
    {
        private readonly ContainerBuilder containerBuilder;

        public AspNetCoreAuthorizationBuilder(IConfigurationRoot configuration, ContainerBuilder containerBuilder)
        {
            //set the connection string from configuration
            var settings = configuration.GetSection("Authorization").Get<AuthorizationSettings>();
            RedisConnectionProvider.ConnectionString = () => settings.PermissionSetCache.RedisConnectionString;

            this.containerBuilder = containerBuilder;
        }

        public void AddAuthorizationServices()
        {
            containerBuilder.RegisterModule<AuthorizationModule>();
            containerBuilder.RegisterType<DefaultPermissionHandler>().As<IAuthorizationHandler>().SingleInstance();
            containerBuilder.RegisterType<AuthorizationCookieAuthEventSubscriber>().AsSelf().SingleInstance();
        }
    }
}
