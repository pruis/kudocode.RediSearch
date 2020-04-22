using System.Linq;
using Autofac;
using KudoCode.LogicLayer.Infrastructure;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KudoCode.LogicLayer.Infrastructure.Dtos.Messages;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Outbound;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;

namespace KudoCode.Test.Unit.CompanyRediSs.Search
{
	[TestClass]
	public class SearchRediSValidationUnitTest : UnitTestBase
	{
		private SearchRediSRequest _request;
		private IValidationContext<SearchRediSResponse> _getResponse;

		public SearchRediSValidationUnitTest()
		{
		}

		[TestMethod]
		public void SearchRediSRequestValidation()
		{
			base.Run(
				"SearchRediSRequest Validation",
				"",
				"",
				"");
		}

		protected override void Seed()
		{
			ApplicationContext.Container.Resolve<IAuthenticationContext<SearchRediSResponse>>()
				  .IsValidTokenProvided = true;

		}

		protected override void Given()
		{
			_request = new SearchRediSRequest() {IndexName = "test", SearchText = "test" };
		}

		protected override void When()
		{
			_getResponse = ApplicationContext
				.Container
				.Resolve<IHandler<SearchRediSRequest, IValidationContext<SearchRediSResponse>>>()
				.Handle(_request);
		}

		protected override void Then()
		{
			Assert.IsFalse(_getResponse.Messages.Any(a =>
			   a.Type == MessageDtoType.Error));
		}
	}
}
