using System;
using System.Collections.Generic;
using Autofac;
using KudoCode.CodeGenerator.Logic.CodeGen.Handlers.Create;
using KudoCode.CodeGenerator.Repository;
using KudoCode.CodeGenerator.Service;
using KudoCode.CodeGenerator.Service.Handlers;
using KudoCode.LogicLayer.Infrastructure;
using KudoCode.LogicLayer.Infrastructure.Domain.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Execution.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Execution.PipeLines;
using KudoCode.LogicLayer.Infrastructure.Messages;

namespace CodeGenerator.Service
{
    public static class ContainerInstaller
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacInfrastructure());
            builder.RegisterModule(new AutoMapperModule());

            builder.RegisterType<GenerateHandler>().As<IGenerate>();

            builder.RegisterType<MessageProxyCollection>().As<IMessageCollection>().SingleInstance();
            builder.RegisterType<ReadOnlyRepository>().As<IReadOnlyRepository>().SingleInstance();
            builder.RegisterType<Repository>().As<IRepository>().SingleInstance();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Handler"))
                .AsImplementedInterfaces().AsSelf()
                ;

            builder.RegisterType<ExecutionPipelineHandlers>()
                .As<IExecutionPipeline>()
                .AsSelf()
                .WithParameter("types", new List<Type>()
                {
                    typeof(CreateFileDtoWorkerHandler),
                    typeof(OutputFileDtoWorkerHandler),
                }).InstancePerLifetimeScope();

            builder.RegisterModule(new CodeGenSettingsModule());

            var container = builder.Build();
            return container;
        }
    }
}