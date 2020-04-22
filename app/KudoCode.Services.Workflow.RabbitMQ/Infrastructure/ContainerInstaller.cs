using System;
using System.Collections.Generic;
using Autofac;
using AutoMapper;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers.Interface;
using KudoCode.LogicLayer.Infrastructure.Dtos.Tokens;
using KudoCode.Web.Api.Connector;
using KudoCode.Web.Infrastructure.Domain;
using KudoCode.Web.Infrastructure.Domain.Execution;
using KudoCode.Web.Infrastructure.Domain.HttpConnector;
using KudoCode.Web.Infrastructure.Domain.HttpConnector.AutofacModule;
using Microsoft.Extensions.Configuration;

namespace Core.Services.Workflow.RabbitMQ.Infrastructure
{
	public static class ContainerInstaller
	{
		public static bool IsBuild { get; private set; }


		public static IConfigurationRoot BuildConfig()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();
			return builder.Build();
		}


		public static void Build()
		{
			if (IsBuild)
				return;

			var builder = new ContainerBuilder();

			builder.RegisterInstance<IConfiguration>(BuildConfig());

			builder.RegisterType<EventExecutionPipeLine>().AsSelf().InstancePerDependency();
			builder.RegisterType<Connector>().AsSelf().SingleInstance();

			builder.RegisterType<ApiPostController>().AsSelf();

			builder.RegisterType<HttpHandler>().AsSelf();
			builder.RegisterType<ApplicationContextHandler>().As<IApplicationContextHandler>();
			builder.RegisterType<ApplicationUserContext>().As<IApplicationUserContext>();

			builder.RegisterModule(new KudoCodeLocalStorageModule());
			builder.RegisterModule(new AutofacAutoMapperModule());



			builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
				.Where(t => t.Name.EndsWith("EventHandler"))
				.AsImplementedInterfaces()
				.InstancePerDependency();

			ApplicationContext.Container = builder.Build();
			IsBuild = true;
		}
	}

}