using System.Linq;
using KudoCode.LogicLayer.Dtos.Lookups;
using KudoCode.LogicLayer.Infrastructure.Dtos.Messages;
using KudoCode.LogicLayer.Infrastructure.Dtos.Responses.Interfaces;
using KudoCode.Web.Api.Connector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.BDDfy;

namespace KudoCode.Test.Acceptance.Lookups
{
	public class LookupsGetBddfy
	{
		private Connector _connector;
		private GetLookupRequest _dto;
		private IApiResponseContextDto<GetLookupResponse> _responseDto;


		public void Get(Connector connector)
		{
			_connector = connector;
			this.Given(_ => GivenALookupRequest())
				.When(_ => WhenIFetchLookups())
				.Then(_ => ThenTheResponseOnFetchIsSuccessful())
				.BDDfy();
		}

		public void GivenALookupRequest()
		{
			_dto = new GetLookupRequest(new[]
			{
				new LookupRequest() {Type = "Gender"},
				new LookupRequest() {Type = "Occupation"},
				new LookupRequest() {Type = "CurrentAdvisor"},
			});
		}

		public void WhenIFetchLookups()
		{
			_responseDto = _connector.EndPoint.Request<GetLookupRequest, GetLookupResponse>(_dto);
		}

		public void ThenTheResponseOnFetchIsSuccessful()
		{
			//Assert.IsTrue(_responseDto.Result.Lookups.Any(a => a.Type.Equals("Gender")));
			//Assert.IsTrue(_responseDto.Result.Lookups.Any(a => a.Type.Equals("Occupation")));
			//Assert.IsTrue(_responseDto.Result.Lookups.Any(a => a.Type.Equals("CurrentAdvisor")));

			Assert.IsFalse(_responseDto.Messages.Any(a =>
				   a.Type == MessageDtoType.Error));


		}
	}
}