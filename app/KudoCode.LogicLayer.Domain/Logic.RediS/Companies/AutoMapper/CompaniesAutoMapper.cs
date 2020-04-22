using AutoMapper;
using KudoCode.LogicLayer.Domain.Logic.Companies.Entities;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using Microsoft.Extensions.Configuration;

namespace KudoCode.LogicLayer.Domain.Logic.Budget.FinancePeriods.AutoMapper
{
	public class CompaniesAutoMapper : Profile
	{
		public CompaniesAutoMapper(IConfiguration configuration)
		{
			//Source : Destination

			//INBOUND
			CreateMap<SaveCompanyRequest, Company>();
			CreateMap<Company, CompanySavedEvent>();




			//OUTBOUND
		}
	}
}