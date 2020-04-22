using System.Collections.Generic;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Outbound;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers.Interface;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers;

namespace KudoCode.LogicLayer.Domain.Logic.CompanyRediSs.Handlers.Search
{

	public class SearchRediSAuthorizationHandler 
		: AbstractAuthorizationHandler<SearchRediSRequest, SearchRediSResponse>
	{

		public SearchRediSAuthorizationHandler(
				IApplicationUserContext applicationUserContext, 
				IAuthorizationContext<SearchRediSResponse> context)
			: base(applicationUserContext, context)
		{
			AllowAnonymous = true;
		}
	}
}

