using Autofac;
using App.Security.Authorization.Data;
using App.Security.Authorization.Infrastructure;
using App.Security.Authorization.Infrastructure.Redis;
using Security.Authorization.Infrastructure.Redis;
using App.Security.Authorization;

namespace App.Security.Registration
{
    public sealed class AuthorizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultBinarySerializer>().As<IBinarySerializer>().SingleInstance();

            builder.RegisterType<RedisPermissionSetCache>().As<IPermissionSetCache>()
                .WithParameter("database", RedisDatabaseBuilder())
                .SingleInstance();

            builder.RegisterType<DefaultAuthorizationContextBuilder>().As<IAuthorizationContextBuilder>();
            builder.RegisterType<DefaultPermissionSetProvider>().As<IPermissionSetProvider>();

            base.Load(builder);
        }
        
        StackExchange.Redis.IDatabase RedisDatabaseBuilder()
        {
            var provider = new RedisConnectionProvider();
            return provider.GetConnection().GetDatabase();
        }
    }
}
