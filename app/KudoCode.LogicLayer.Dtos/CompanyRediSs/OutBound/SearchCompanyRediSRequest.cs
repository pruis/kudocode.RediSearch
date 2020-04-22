using KudoCode.LogicLayer.Infrastructure.Dtos;
using KudoCode.LogicLayer.Infrastructure.Dtos.Requests.Interfaces;

namespace KudoCode.LogicLayer.Dtos.CompanyRediSs.Outbound
{
    public class SearchRediSRequest : IApiRequestDto
    {
        public SearchRediSRequest()
        {
            Count = 10;
            Offset = 0;
        }

        public string SearchText { get; set; }
        public int Count { get; set; }
        public int Offset { get; set; }
        public string IndexName { get; set; }
    }
}
