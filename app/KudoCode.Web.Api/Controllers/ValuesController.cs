using KudoCode.LogicLayer.Infrastructure.Execution.PipeLines;
using Microsoft.AspNetCore.Mvc;

namespace KudoCode.Web.Api.Controllers
{
	//[Authorization()]
	[Route("api/[controller]")]
    public class ValuesController : Controller
    {
	    private readonly ExecutionPipeline _executionPlan;

	    public ValuesController(ExecutionPipeline executionPlan)
	    {
		    _executionPlan = executionPlan;
	    }

		// GET api/values
        [HttpGet]
        public string Get()
        {

			//var roundDto = new CreateRoundDto();
			//_executionPlan.Execute<CreateRoundDto, ResponseDto<int>>(roundDto);
		//	var result = _executionPlan.Execute<GetRoundsDto, ResponseDto<List<GetRoundsDto>>>(new GetRoundsDto());

	        return "test";
	        //		return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
