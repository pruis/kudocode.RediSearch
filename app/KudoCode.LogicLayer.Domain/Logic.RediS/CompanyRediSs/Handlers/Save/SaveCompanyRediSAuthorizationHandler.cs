using System.Collections.Generic;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Inbound;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers.Interface;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers;

namespace KudoCode.LogicLayer.Domain.Logic.CompanyRediSs.Handlers.Save
{

	public class SaveCompanyRediSAuthorizationHandler
		: AbstractAuthorizationHandler<SaveCompanyRediSRequest, SaveCompanyRediSResponse>
	{

		public SaveCompanyRediSAuthorizationHandler(
			IApplicationUserContext applicationUserContext,
			IAuthorizationContext<SaveCompanyRediSResponse> context)
		: base(applicationUserContext, context)
		{
			AllowAnonymous = true;
		}
	}
}

