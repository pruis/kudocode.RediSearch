using AutoMapper;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Inbound;
using KudoCode.Messaging.Infrastructure;
using KudoCode.Web.Infrastructure.Domain.HttpConnector;

namespace Core.Services.Workflow.RabbitMQ.Handler
{
	public class CompanySavedEventHandler : EventHandler<CompanySavedEvent>
	{
		private ApiPostController _apiPostController;
		private readonly IMapper _mapper;

		public CompanySavedEventHandler(ApiPostController apiPostController, IMapper mapper)
		{
			_apiPostController = apiPostController;
			_mapper = mapper;
		}

		public override void Execute()
		{
			_apiPostController
				.Create<SaveCompanyRediSRequest,SaveCompanyRediSResponse>()
				.Post(_mapper.Map<SaveCompanyRediSRequest>(Event.MetaData));
		}
	}
}