using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Autofac;
using Autofac.Extras.Quartz;
using KudoCode.Services.Scheduler.Quartz.Handlers.SendEmails;
using KudoCode.Web.Api.Connector;
using Microsoft.Extensions.Configuration;

namespace KudoCode.Services.Scheduler.Quartz.Service
{
    public static class ContainerInstaller
    {
        public static IContainer BuildContainer()
        {
            //Read config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json", optional: false, reloadOnChange: true)
                .Build();

            //Start building container

            var builder = new ContainerBuilder();

            builder.RegisterInstance(config).As<IConfiguration>();

            builder.RegisterType<Connector>().AsSelf();
            builder.RegisterType<ManageScheduler>().AsSelf();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("ScheduleHandler"))
                .AsImplementedInterfaces()
                ;

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Schedule"))
                .AsImplementedInterfaces().AsSelf()
                ;

            builder.RegisterType<SchedulerService>();

            var schedulerConfig = new NameValueCollection
            {
                {"quartz.scheduler.instanceName", "MyScheduler"},
                {"quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz"},
                {"quartz.threadPool.threadCount", "3"}
            };

            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });

            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(SendEmailScheduleHandler).Assembly));
            string connectionString = String.Empty;
            if (Environment.MachineName.Equals("DESKTOP-ARBABNG", StringComparison.InvariantCultureIgnoreCase))
                connectionString = ("data source=DESKTOP-ARBABNG;Initial Catalog=kudocodeRediSTestDB;Integrated Security=SSPI;");
            else if (Environment.MachineName.Equals("", StringComparison.InvariantCultureIgnoreCase))
                connectionString = ("data source=DESKTOP-JQMQ508;Initial Catalog=kudocodeRediSTestDB;Integrated Security=SSPI;");

            builder
                .RegisterType<SqlConnection>()
                .WithParameter("connectionString", connectionString)
                .As<IDbConnection>()
                .InstancePerMatchingLifetimeScope(QuartzAutofacFactoryModule.LifetimeScopeName);

            // Other registrations

            var container = builder.Build();
            return container;
        }
    }
}