using Autofac;
using KudoCode.LogicLayer.Dtos;
using Core.Services.Workflow.RabbitMQ.Infrastructure;
using KudoCode.Messaging.Infrastructure.Interfaces;

namespace KudoCode.Web.Api
{
    public class AutoFacWebApi : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Constants.EventQueues.Queue.ForEach(source =>
                builder.RegisterType<RabbitMqManager>()
                    .As<IEventManager>()
                    .Named(source, typeof(IEventManager))
                    .WithParameter(new TypedParameter(typeof(string), source))
                    .SingleInstance()
            );
        }
    }
}