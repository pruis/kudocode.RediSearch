using System.Collections.Generic;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers.Interface;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers;

namespace KudoCode.LogicLayer.Domain.Logic.Companies.Handlers.Save
{

	public class SaveCompanyAuthorizationHandler : AbstractAuthorizationHandler<SaveCompanyRequest, SaveCompanyResponse>
	{

		public SaveCompanyAuthorizationHandler(IApplicationUserContext applicationUserContext, IAuthorizationContext<SaveCompanyResponse> context) 
		: base(applicationUserContext, context)
		{
			AllowAnonymous = true;
		}
	}
}

