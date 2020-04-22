using AutoMapper;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Inbound;
using Microsoft.Extensions.Configuration;

namespace KudoCode.LogicLayer.Domain.Logic.Budget.FinancePeriods.AutoMapper
{
	public class CompaniesAutoMapper : Profile
	{
		public CompaniesAutoMapper(IConfiguration configuration)
		{
			//Source : Destination

			//INBOUND
			CreateMap<CompanySavedEvent, SaveCompanyRediSRequest>();

			//OUTBOUND
		}
	}
}