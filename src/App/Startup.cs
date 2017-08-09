using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;

namespace SampleMvcApp
{
    public partial class Startup
    {
        IConfigurationRoot Configuration { get; }

        IContainer Container { get; set; }

        ContainerBuilder ContainerBuilder { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            ContainerBuilder = new ContainerBuilder();

            securityBuilder = new SecurityServicesBuilder(Configuration, ContainerBuilder);
        }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            securityBuilder.WithServices(services)
                .AddAuthentication()
                .AddAuthorization();

            services
                .AddMvc()
                .AddControllersAsServices();

            return ConfigureServiceProvider(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<OpenIdConnectOptions> oidcOptions)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            securityBuilder
                .WithContainer(Container)
                .WithApp(app)
                .WithOpenIdConnectOptions(oidcOptions.Value)
                .UseAuth0();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IServiceProvider ConfigureServiceProvider(IServiceCollection services)
        {
            ContainerBuilder.Populate(services);

            Container = ContainerBuilder.Build();
            return new AutofacServiceProvider(Container);
        }
    }
}
