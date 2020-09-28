using System.Linq;
using Autofac;
using KudoCode.LogicLayer.Infrastructure;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KudoCode.LogicLayer.Infrastructure.Dtos.Messages;
using KudoCode.Test.Unit;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Inbound;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;

namespace KudoCode.Test.Unit.CompanyRediSs.Save
{
	[TestClass]
	public class SaveCompanyRediSValidationUnitTest : UnitTestBase
	{
		private SaveCompanyRediSRequest _request;
		private IValidationContext<SaveCompanyRediSResponse> _getResponse;

		public SaveCompanyRediSValidationUnitTest()
		{
		}

		[TestMethod]
		public void SaveCompanyRediSRequestValidation()
		{
			base.Run(
				"SaveCompanyRediSRequest Validation",
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
				.Resolve<IHandler<SaveCompanyRediSRequest, IValidationContext<SaveCompanyRediSResponse>>>()
				.Handle(_request);
		}

		protected override void Then()
		{
			Assert.IsFalse(_getResponse.Messages.Any(a =>
			   a.Type == MessageDtoType.Error));
		}
	}
}
