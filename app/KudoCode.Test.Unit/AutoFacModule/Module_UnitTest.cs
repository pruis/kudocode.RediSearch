using System;
using Autofac;
using KudoCode.LogicLayer.Infrastructure.Domain.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Domain.Repository;
using KudoCode.Test.Unit.InMemoryDataBase;
using Microsoft.EntityFrameworkCore;

namespace KudoCode.Test.Unit.AutoFacModule
{
    public class Module_UnitTest : Module
    {
        private bool _isTest;

        public Module_UnitTest(bool isTest = false)
        {
            _isTest = isTest;
        }

        protected override void Load(ContainerBuilder builder)
        {
//            Constants.EventSources.Sources.ForEach(source =>
//                builder.RegisterType<RabbitMqManager>()
//                    .As<IEventManager>()
//                    .Named(source, typeof(IEventManager))
//                    .WithParameter(new TypedParameter(typeof(string), source))
//                    .SingleInstance()
//            );

            builder.RegisterType<InMemoryDataContextUnitTest>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<EntityFrameworkRepository<InMemoryDataContextUnitTest>>().As<IRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<EntityFrameworkReadOnlyRepository<InMemoryDataContextUnitTest>>().As<IReadOnlyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("UnitTest"))
                .AsImplementedInterfaces();
        }
    }
}