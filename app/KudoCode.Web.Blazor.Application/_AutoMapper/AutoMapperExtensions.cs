using AutoMapper;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers;
using KudoCode.Web.Blazor.RediS.ViewModel;

namespace KudoCode.Web.Blazor.Application._AutoMapper
{
	public class AutoMapperExtensions : Profile
	{
		public AutoMapperExtensions()
		{
			CreateMap<CompanyViewModel, SaveCompanyRequest>();
			CreateMap<ApplicationUserContext, ApplicationUserContext>();
		}
	}
}