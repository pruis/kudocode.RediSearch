using System.Collections.Generic;
using Autofac;
using KudoCode.LogicLayer.Dtos;
using KudoCode.LogicLayer.Infrastructure.Dtos;
using KudoCode.LogicLayer.Infrastructure.Dtos.Tokens;
using KudoCode.Messaging.Infrastructure.Interfaces;
using KudoCode.Web.Api.Connector;

namespace Core.Services.Workflow.RabbitMQ.Infrastructure
{
    public class WorkFlowService
    {
        private List<IEventManager> _managers;

        public WorkFlowService()
        {
            _managers = new List<IEventManager>();
        }

        public bool Start()
        {
            ContainerInstaller.Build();
            
           var appUserResponse =  ApplicationContext.Container
                .Resolve<Connector>()
                .Authentication
                .GetToken(new GetTokenRequest() {Email = "work@flow.com", Password = "1234"});

            ApplicationContext.Container.Resolve<ITokenCache>().Set(appUserResponse.Result.Token.Value);


            foreach (var queue in Constants.EventQueues.Queue)
                _managers.Add(new RabbitMqManager(queue).Listen());

            return true;
        }

        public bool Stop()
        {
            ApplicationContext.Container?.Dispose();
            _managers.ForEach(a => a?.Dispose());
            return true;
        }
    }
}