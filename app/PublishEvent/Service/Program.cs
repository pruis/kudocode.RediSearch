using System.Collections;
using Autofac;
using KudoCode.LogicLayer.Infrastructure.Dtos.Events;
using KudoCode.LogicLayer.Infrastructure.Dtos.Events.Interfaces;
using KudoCode.Messaging.Infrastructure.Interfaces;

namespace PublishEvent.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var events = new ArrayList();

            //var eventRequestDto = new EventRequestDto<CreatePortfolioImportXslEventMessage>();
            //eventRequestDto.Queues.Add(KudoCode.LogicLayer.Dtos.Constants.EventQueues.InternalEventsQueue);
            //eventRequestDto.MetaData = new CreatePortfolioImportXslEventMessage();
            //events.Add(eventRequestDto);

            //ApplicationContext.Container = ContainerInstaller.BuildContainer();

            //foreach (var @event in events)
            //foreach (var source in (@event as IEventRequestSources).Queues)
            //    ApplicationContext.Container.ResolveNamed<IEventManager>(source).Send(@event);
        }
    }
}