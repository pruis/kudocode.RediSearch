using KudoCode.LogicLayer.Dtos.Lookups;
using KudoCode.LogicLayer.Infrastructure.Dtos.Responses.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Execution.PipeLines;
using Microsoft.AspNetCore.Mvc;

namespace KudoCode.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Lookups")]
	[TokenAuthentication]
	public class LookupsController : Controller
    {
	    private readonly ExecutionPipeline _executionPlan;

	    public LookupsController(ExecutionPipeline executionPlan )
	    {
		    _executionPlan = executionPlan;
	    }

	    [HttpPost]
	    [Route("[action]")]
	    public IApiResponseContextDto<GetLookupResponse> Get([FromBody] GetLookupRequest dto)
	    {
		    var result = _executionPlan.Execute<GetLookupRequest, GetLookupResponse>(dto);
		    return result;
	    }
	}
}