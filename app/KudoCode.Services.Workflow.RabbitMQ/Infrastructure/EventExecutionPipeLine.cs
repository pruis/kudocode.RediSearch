using System;
using Autofac;
using KudoCode.Messaging.Infrastructure.Interfaces;

namespace Core.Services.Workflow.RabbitMQ.Infrastructure
{
    public class EventExecutionPipeLine : IEventExecutionPipeLine
    {
        public void Execute(object requestDto)
        {
            try
            {
                var eventHandler = ApplicationContext.Container
                    .Resolve(typeof(IEventHandler<>)
                        .MakeGenericType(requestDto.GetType().GetGenericArguments()[0]));

                eventHandler.GetType()
                    .GetMethod("Handle")
                    .Invoke(eventHandler, new[] {requestDto});
            }
            catch (Exception e)
            {
                //Log error and send email
                throw;
            }
        }
    }
}