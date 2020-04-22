using AutoMapper;
using KudoCode.LogicLayer.Infrastructure.Dtos.Responses.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Execution.Context;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;

namespace CodeGenerator.Service
{
    public class CodeGenAutoMapper : Profile
    {
        public CodeGenAutoMapper()
        {
        }
    }

    public class ApiResponseContextDtoAutoMapper : Profile
    {
        public ApiResponseContextDtoAutoMapper()
        {

            //CreateMap(typeof(IExecutionPipelineContext<>), typeof(IApiResponseContextDto<>));
            CreateMap(typeof(ExecutionPipelineContext<>), typeof(IApiResponseContextDto<>));

            //CreateMap<IExecutionPipelineContext<>, IApiResponseContextDto<>>();
            //CreateMap<ApplicationUserDtoBasic, ApplicationUser>().ReverseMap();
            //CreateMap<IExecutionPipelineContext<>, IExecutionPipelineContext<> >().ReverseMap();
        }
    }
}