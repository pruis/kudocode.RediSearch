using System.Linq;
using Autofac;
using KudoCode.LogicLayer.Infrastructure;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KudoCode.LogicLayer.Infrastructure.Dtos.Messages;
using KudoCode.Test.Unit;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Outbound;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;

namespace KudoCode.Test.Unit.CompanyRediSs.Search
{
	[TestClass]
	public class SearchRediSAuthorizationUnitTest : UnitTestBase
	{
		private SearchRediSRequest _request;
		private IAuthorizationContext<SearchRediSResponse> _getResponse;

		public SearchRediSAuthorizationUnitTest()
		{
		}

		[TestMethod]
		public void SearchRediSRequestAuthorization()
		{
			base.Run(
				"SearchRediSRequest Authorization",
				"",
				"",
				"");
		}

		protected override void Seed()
		{

		}

		protected override void Given()
		{
			_request = new SearchRediSRequest() 
			{ 
				SearchText = "test",
			};
		}

		protected override void When()
		{
			_getResponse = ApplicationContext
				.Container
				.Resolve<IHandler<
						SearchRediSRequest, 
						IAuthorizationContext<SearchRediSResponse>>
					>()
				.Handle(_request);
		}

		protected override void Then()
		{
			Assert.IsFalse(_getResponse.Messages.Any(a =>
			   a.Type == MessageDtoType.Error));
		}
	}
}
