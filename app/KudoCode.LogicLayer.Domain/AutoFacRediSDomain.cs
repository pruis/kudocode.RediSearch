using System;
using Autofac;
using KudoCode.LogicLayer.Infrastructure.Handlers.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Messages;
using KudoCode.LogicLayer.Plugin.Redis.Domain.Logic.Redis.Plugin;
using KudoCode.LogicLayer.Plugin.Redis.Infrastructure;

namespace KudoCode.LogicLayer.Domain.Logic.RediS
{
    public class AutoFacRedisDomain : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DelegateContext>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterGeneric(typeof(RedisQueryHandlerPlugin<,,>))
                .As(typeof(IQueryHandlerDelegates<,,>))
                .PropertiesAutowired().InstancePerLifetimeScope();

            builder
                .RegisterGeneric(typeof(RedisCommandHandlerPlugin<,,>))
                .As(typeof(ICommandHandlerDelegates<,,>))
                .PropertiesAutowired().InstancePerLifetimeScope();

            builder
                .RegisterType<MessageProxyCollection>()
                .As<IMessageCollection>()
                .SingleInstance();

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Handler"))
                .AsImplementedInterfaces().AsSelf().PropertiesAutowired();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("AuditDefinition"))
                .AsImplementedInterfaces().AsSelf().PropertiesAutowired();

        }
    }
}