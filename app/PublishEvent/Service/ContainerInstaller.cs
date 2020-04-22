using Autofac;
using Core.Services.Workflow.RabbitMQ.Infrastructure;
using KudoCode.Messaging.Infrastructure.Interfaces;
using IContainer = Autofac.IContainer;

namespace PublishEvent.Service
{
    public static class ContainerInstaller
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            KudoCode.LogicLayer.Dtos.Constants.EventQueues.Queue.ForEach(source =>
                builder.RegisterType<RabbitMqManager>()
                    .As<IEventManager>()
                    .Named(source, typeof(IEventManager))
                    .WithParameter(new TypedParameter(typeof(string), source))
                    .SingleInstance()
            );
            var container = builder.Build();
            return container;
        }
    }
}