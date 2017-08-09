using App.Security.AspNet.Authentication;
using App.Security.AspNet.Authentication.Auth0;
using Autofac;

namespace App.Security.AspNet.Registration
{
    public class AspNetAuthenticationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LockContextFactory>().As<ILockContextFactory>();
            builder.RegisterType<AuthenticationHandler>().As<IAuthenticationHandler>();
        }
    }
}
