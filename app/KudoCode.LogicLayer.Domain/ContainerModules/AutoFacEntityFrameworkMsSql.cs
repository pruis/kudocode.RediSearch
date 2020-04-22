using Autofac;
using KudoCode.LogicLayer.Domain.Repository;
using KudoCode.LogicLayer.Infrastructure.Domain.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace KudoCode.LogicLayer.Domain.ContainerModules
{
    public class AutoFacEntityFrameworkMsSql : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>().AsSelf().As<DbContext>()
                .InstancePerMatchingLifetimeScope("ExecutionPipeline");
            builder.RegisterType<EntityFrameworkRepository<DataContext>>().As<IRepository>()
                .InstancePerMatchingLifetimeScope("ExecutionPipeline")
                ;
            builder.RegisterType<EntityFrameworkReadOnlyRepository<DataContext>>().As<IReadOnlyRepository>()
                .InstancePerMatchingLifetimeScope("ExecutionPipeline")
                ;
        }
    }
}