using App.Security.AspNet.Auth0;
using App.Security.AspNet.Authentication;
using App.Security.AspNet.Authorization;
using App.Security.AspNet.Authorization.Extensions;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Authorization.Model;
using System.Collections.Generic;

namespace SampleMvcApp
{
    public partial class Startup
    {
        readonly SecurityServicesBuilder securityBuilder;

        /// <summary>
        /// Helper class found in partial class file Startup.Security
        /// </summary>
        private class SecurityServicesBuilder
        {
            readonly Auth0AuthenticationBuilder auth0;
            private AspNetCoreAuthorizationBuilder authorization;

            private ContainerBuilder containerBuilder;
            private IServiceCollection services;
            private IApplicationBuilder app;
            private OpenIdConnectOptions oidcOptions;
            private IContainer container;

            public SecurityServicesBuilder(IConfigurationRoot configuration, ContainerBuilder containerBuilder)
            {
                auth0 = new Auth0AuthenticationBuilder(configuration);
                authorization = new AspNetCoreAuthorizationBuilder(configuration, containerBuilder);

                this.containerBuilder = containerBuilder;
            }

            public SecurityServicesBuilder WithServices(IServiceCollection services)
            {
                this.services = services;
                return this;
            }

            public SecurityServicesBuilder WithContainer(IContainer container)
            {
                this.container = container;
                return this;
            }

            public SecurityServicesBuilder WithApp(IApplicationBuilder app)
            {
                this.app = app;
                return this;
            }

            public SecurityServicesBuilder WithOpenIdConnectOptions(OpenIdConnectOptions options)
            {
                this.oidcOptions = options;
                return this;
            }

            public SecurityServicesBuilder AddAuthentication()
            {
                auth0.Using(services).AddAuth0();
                return this;
            }

            public SecurityServicesBuilder AddAuthorization()
            {
                authorization.AddAuthorizationServices();

                var permissions = GetPermissions();
                services.AddPermissionPolicies(permissions);
                return this;
            }

            public void UseAuth0()
            {
                var subscriber = container.Resolve<AuthorizationCookieAuthEventSubscriber>();
                app.UseAuth0(this.oidcOptions, subscriber);
            }

            private IEnumerable<Permission> GetPermissions()
            {
                return new List<Permission>();
            }
        }
    }
}
