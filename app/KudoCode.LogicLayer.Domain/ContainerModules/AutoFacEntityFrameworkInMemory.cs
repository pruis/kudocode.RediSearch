using Autofac;
using KudoCode.LogicLayer.Domain.Repository;
using KudoCode.LogicLayer.Infrastructure.Domain.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace KudoCode.LogicLayer.Domain.ContainerModules
{
    public class AutoFacEntityFrameworkInMemory : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryDataContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<EntityFrameworkRepository<InMemoryDataContext>>().As<IRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<EntityFrameworkReadOnlyRepository<InMemoryDataContext>>().As<IReadOnlyRepository>()
                .InstancePerLifetimeScope();
        }
    }
}