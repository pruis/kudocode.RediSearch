using Autofac;
using AutoMapper;
using KudoCode.LogicLayer.Infrastructure.Domain.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers;
using KudoCode.LogicLayer.Domain.Logic.Companies.Entities;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Dtos;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Inbound;

namespace KudoCode.LogicLayer.Domain.Logic.Companies.Handlers.Save
{
	public class SaveCompanyWorkerHandler : CommandHandler<SaveCompanyRequest,
		Company, SaveCompanyResponse>
	{
		public SaveCompanyWorkerHandler(
			IMapper mapper,
			IRepository repository,
			ILifetimeScope scope,
			IWorkerContext<SaveCompanyResponse> context)
			: base(mapper, repository, scope, context)
		{
		}

		protected override void GetEntity()
		{
			Entity = Request.Id > 0
				? Repository.GetOne<Company>(a => a.Id == Request.Id)
				: new Company();
		}
		protected override void ValidateEntity()
		{
			if (Request.Id > 0 && Entity == null)
				Context.AddMessage("E4", "Company");
		}
		protected override void Execute()
		{
			// Map dto to entity and persist to DB
			Mapper.Map(Request, Entity);
			Repository.Save(Entity);
			Repository.SaveChanges();

			//setup response
			if (Context.Result == null)
				Context.Result = new SaveCompanyResponse();
			Context.Result.Id = Entity.Id;

			//Raise CompanySavedEvent event to service bus
			Context.RaiseEvent(
				Mapper.Map<CompanySavedEvent>(Entity),
				Constants.EventQueues.InternalEventsQueue
				);

			//// ** Raises an event to raise the SaveCompanyRediSAgg 
			//// ** via http using a generic Event Handler

			//Context.RaiseAggregate
			//	<SaveCompanyRediSRequest, SaveCompanyRediSResponse>(
			//		Mapper.Map<SaveCompanyRediSRequest>(Request),
			//		Constants.EventQueues.AggregateEventsQueue);


		}
	}
}
