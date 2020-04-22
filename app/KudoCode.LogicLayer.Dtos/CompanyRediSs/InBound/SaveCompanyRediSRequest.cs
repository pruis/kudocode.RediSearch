using KudoCode.LogicLayer.Infrastructure.Dtos;
using KudoCode.LogicLayer.Infrastructure.Dtos.Requests.Interfaces;

namespace KudoCode.LogicLayer.Dtos.CompanyRediSs.Inbound
{
    public class SaveCompanyRediSRequest : IApiRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
	}
}
