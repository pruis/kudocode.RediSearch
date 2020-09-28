using System.Linq;
using Autofac;
using KudoCode.LogicLayer.Infrastructure;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KudoCode.LogicLayer.Infrastructure.Dtos.Messages;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Inbound;

namespace KudoCode.Test.Unit.CompanyRediSs.Save
{
	[TestClass]
	public class SaveCompanyRediSAuthorizationUnitTest : UnitTestBase
	{
		private SaveCompanyRediSRequest _request;
		private IAuthorizationContext<SaveCompanyRediSResponse> _getResponse;

		public SaveCompanyRediSAuthorizationUnitTest()
		{
		}

		[TestMethod]
		public void SaveCompanyRediSRequestAuthorization()
		{
			base.Run(
				"SaveCompanyRediSRequest Authorization",
				"",
				"",
				"");
		}

		protected override void Seed()
		{

		}

		protected override void Given()
		{
			_request = new SaveCompanyRediSRequest() { };
		}

		protected override void When()
		{
			_getResponse = ApplicationContext
				.Container
				.Resolve<IHandler<SaveCompanyRediSRequest, IAuthorizationContext<SaveCompanyRediSResponse>>>()
				.Handle(_request);
		}

		protected override void Then()
		{
			Assert.IsFalse(_getResponse.Messages.Any(a =>
			   a.Type == MessageDtoType.Error));
		}
	}
}
