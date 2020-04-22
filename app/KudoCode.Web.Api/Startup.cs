using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using KudoCode.Helpers;
using KudoCode.LogicLayer.Domain;
using KudoCode.LogicLayer.Domain.ContainerModules;
using KudoCode.LogicLayer.Domain.Repository;
using KudoCode.LogicLayer.Infrastructure;
using KudoCode.LogicLayer.Infrastructure.Domain;
using KudoCode.LogicLayer.Plugin.EntityAudit.Domain.Logic.EntityAudits.AutofacModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using KudoCode.LogicLayer.Domain.Logic.RediS;

namespace KudoCode.Web.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();

            JsonConvert.DefaultSettings = Json.Serialization.Auto;
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; private set; }

        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the Configure method, below.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                options => options.EnableEndpointRouting = false
            ).AddNewtonsoftJson(opt =>
                opt.SerializerSettings.TypeNameHandling = Json.Serialization.Auto().TypeNameHandling);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        );
            });
            services.AddHttpContextAccessor();
            SetupContainer(services);

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        private void SetupContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterInstance<IConfiguration>(Configuration);

            builder.RegisterModule(new AutofacInfrastructure());
            builder.RegisterModule(new AutoFacInfrastructureDomain());

            builder.RegisterModule(new AutoFacEntityFrameworkMsSql());
//			builder.RegisterModule(new AutoFacEntityFrameworkInMemory());

            builder.RegisterModule(new AutofacEntityAudit());

            builder.RegisterModule(new AutoFacAutoMapperModule());
            builder.RegisterModule(new AutoFacWebApi());
            builder.RegisterModule(new AutoFacRedisDomain());


            var container = builder.Build();
            this.ApplicationContainer = container;
            ApplicationContext.Container = container;

            InMemoryDataContext.DbName = "InMemory";
            MockDataBase.InitializeDataBase();
        }

        // Configure is where you add middleware. This is called after
        // ConfigureServices. You can use IApplicationBuilder.ApplicationServices
        // here if you need to resolve things from the container.
        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            //loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseExceptionHandler("/error");

            app.UseCors("CorsPolicy");
            app.UseCorsOptions();

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
                
            app.UseMvc();
            loggerFactory.AddLog4Net();


            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            // You can only do this if you have a direct reference to the container,
            // so it won't work with the above ConfigureContainer mechanism.
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }


    namespace Web.Middlewares
    {
        //https://stackoverflow.com/questions/42199757/enable-options-header-for-cors-on-net-core-web-api
    }
}