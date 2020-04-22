using KudoCode.LogicLayer.Infrastructure.Dtos.Responses.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Execution.Interfaces;
using KudoCode.LogicLayer.Plugin.EntityAudit.Dtos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KudoCode.Web.Api.Controllers.EntityAudit
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EntityAuditController : Controller
    {
        private readonly IExecutionPipeline _executionPipeline;

        public EntityAuditController(IExecutionPipeline executionPipeline)
        {
            _executionPipeline = executionPipeline;
        }

        [HttpPost]
        [Route("[action]")]
        public IApiResponseContextDto<int> Create(ICreateEntityAuditDto createEntityAuditDto)
        {
            return _executionPipeline.Execute<ICreateEntityAuditDto, int>(createEntityAuditDto);
        }
    }
}