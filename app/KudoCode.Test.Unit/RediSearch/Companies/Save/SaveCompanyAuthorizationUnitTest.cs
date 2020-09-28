using System.Linq;
using Autofac;
using KudoCode.LogicLayer.Infrastructure;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KudoCode.LogicLayer.Infrastructure.Dtos.Messages;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;

namespace KudoCode.Test.Unit.Companies.Save
{
	[TestClass]
	public class SaveCompanyAuthorizationUnitTest : UnitTestBase
	{
		private SaveCompanyRequest _request;
		private IAuthorizationContext<SaveCompanyResponse> _getResponse;

		public SaveCompanyAuthorizationUnitTest()
		{
		}

		[TestMethod]
		public void SaveCompanyRequestAuthorization()
		{
			base.Run(
				"SaveCompanyRequest Authorization",
				"",
				"",
				"");
		}

		protected override void Seed()
		{
		}

		protected override void Given()
		{
			_request = new SaveCompanyRequest() { };
		}

		protected override void When()
		{
			_getResponse = ApplicationContext
				.Container
				.Resolve<IHandler<SaveCompanyRequest, IAuthorizationContext<SaveCompanyResponse>>>()
				.Handle(_request);
		}

		protected override void Then()
		{
			Assert.IsFalse(_getResponse.Messages.Any(a =>
			   a.Type == MessageDtoType.Error));
		}
	}
}
