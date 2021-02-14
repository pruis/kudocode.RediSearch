using System.Threading.Tasks;
using KudoCode.LogicLayer.Infrastructure;
using KudoCode.LogicLayer.Infrastructure.Dtos.Requests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace KudoCode.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Generic")]
    [EnableCors("CorsPolicy")] 
    public class GenericController : Controller
    {
        private readonly ApiControllerRequestManager _apiControllerRequestManager;

        public GenericController(ApiControllerRequestManager apiControllerRequestManager)
        {
            _apiControllerRequestManager = apiControllerRequestManager;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> Post([FromBody] ApiControllerRequestDto dto)
        {
            return await _apiControllerRequestManager.Execute(dto);
        }
    }
}