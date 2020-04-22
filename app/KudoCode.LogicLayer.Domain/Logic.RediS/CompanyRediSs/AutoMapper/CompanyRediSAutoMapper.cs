using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace KudoCode.LogicLayer.Domain.Logic.Budget.FinancePeriods.AutoMapper
{
	public class CompanyRediSAutoMapper : Profile
	{
		public CompanyRediSAutoMapper(IConfiguration configuration)
		{
			//Source : Destination

			//INBOUND
			//CreateMap<SaveCompanyRediSRequest, GetRediSResponse>()
			//		.ForMember(dst => dst.Id, o => o.MapFrom(src => src.Id.ToString()))
					//.ForMember(dst => dst.Name, o => o.MapFrom(src => src.Name))
					//.ForMember(dst => dst.Description, o => o.MapFrom(src => src.Description))
					//;
			//OUTBOUND
		}
	}
}