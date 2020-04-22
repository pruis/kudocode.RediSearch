using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers;
using KudoCode.LogicLayer.Infrastructure.Dtos.Responses.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Dtos.Tokens;
using KudoCode.LogicLayer.Infrastructure.Execution.PipeLines;
using Microsoft.AspNetCore.Mvc;

namespace KudoCode.Web.Api.Controllers
{
	[Route("api/[controller]")]
	public class AuthenticationController : Controller
	{
		private readonly ExecutionPipeline _executionPlan;

		public AuthenticationController(ExecutionPipeline executionPlan)
		{
			_executionPlan = executionPlan;
		}

		[HttpPost]
		[Route("[action]")]
		public IApiResponseContextDto<ApplicationUserContext> RequestToken([FromBody] GetTokenRequest request)
		{
			var result = _executionPlan.Execute<GetTokenRequest, ApplicationUserContext>(request);
			return result;
		}

		[HttpPost]
		[Route("[action]")]
		public IApiResponseContextDto<ApplicationUserContext> Register([FromBody] RegisterApplicationUserDto model)
		{
			var result = _executionPlan.Execute<RegisterApplicationUserDto, ApplicationUserContext>(model);
			return result;
		}

	}


}