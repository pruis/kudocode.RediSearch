using System.Linq;
using Autofac;
using KudoCode.LogicLayer.Infrastructure;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Outbound;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Inbound;
using System.Collections.Generic;
using Newtonsoft.Json;
using KudoCode.LogicLayer.Dtos.Companies.Outbound;
using KudoCode.LogicLayer.Infrastructure.Dtos.Messages;

namespace KudoCode.Test.Unit.CompanyRediSs.Search
{
	[TestClass]
	public class SearchRediSWorkerUnitTest : UnitTestBase
	{
		//private SearchRediSRequest _request;
		private SearchRediSRequest _req_Lead;
		//private SearchRediSRequest _req_Test;
		//private SearchRediSRequest _req_NameAndDescription;

		private IWorkerContext<SearchRediSResponse> _rsp_Lead;
		//private IWorkerContext<SearchRediSResponse> _rsp_Test;
		//private IWorkerContext<SearchRediSResponse> _rsp_NameAndDescription;

		[TestMethod]
		public void SearchRediSRequestWorker()
		{
			base.Run(
				"SearchRediSRequest Worker",
				"",
				"",
				"");
		}

		protected override void Seed()
		{
			PopulateTestData();
		}
		protected override void Given()
		{
			_req_Lead = new SearchRediSRequest()
			{ IndexName = "GetCompany", SearchText = "lead*" };

			//_req_Test = new SearchRediSRequest()
			//{ IndexName = "GetCompany", SearchText = "test" };

			//_req_NameAndDescription = new SearchRediSRequest()
			//{ IndexName = "GetCompany", SearchText = $"(@Name:test)|(@Description:services)" };
		}
		protected override void When()
		{
			_rsp_Lead = ApplicationContext
				.Container
				.Resolve<IHandler
					<SearchRediSRequest,
					IWorkerContext<SearchRediSResponse>>>()
				.Handle(_req_Lead);

			//_rsp_Test = ApplicationContext
			//	.Container
			//	.Resolve<IHandler
			//		<SearchRediSRequest,
			//		IWorkerContext<SearchRediSResponse>>>()
			//	.Handle(_req_Test);

			//_rsp_NameAndDescription = ApplicationContext
			//	.Container
			//	.Resolve<IHandler
			//		<SearchRediSRequest,
			//		IWorkerContext<SearchRediSResponse>>>()
			//	.Handle(_req_NameAndDescription);

		}
		protected override void Then()
		{

			//Assert.IsFalse(_rsp_Test.Messages.Any(a =>
			//   a.Type == MessageDtoType.Error));

			//Assert.IsFalse(_rsp_NameAndDescription.Messages.Any(a =>
			//a.Type == MessageDtoType.Error));


			Assert.IsFalse(_rsp_Lead.Messages.Any(a =>
			   a.Type == MessageDtoType.Error));

			Assert.IsTrue(_rsp_Lead.Result.List.Count == 2);

			Assert.IsTrue(
				_rsp_Lead.Result.List.Any(
					a => a.Data.Name.ToString().Contains("lead")));

			Assert.IsTrue(
				_rsp_Lead.Result.List.Any(
					a => a.Data.Description.ToString().Contains("leaders")));
		}
		private static void PopulateTestData()
		{
			var list = new List<SaveCompanyRediSRequest>()
			{
				//new SaveCompanyRediSRequest
				//{
				//	Id= 1,
				//	Name = "Acme",
				//	Description = "this is company number one test"
				//}
				//,new SaveCompanyRediSRequest
				//{
				//	Id= 2,
				//	Name = "Acme test",
				//	Description = "this is company number two"
				//}
				//,
				new SaveCompanyRediSRequest
				{
					Id= 3,
					Name = "Globex lead",
					Description = "we offer financial services"
				}
				,new SaveCompanyRediSRequest
				{
					Id= 4,
					Name = "Globex (pty)",
					Description = "Global communications industry leaders"
				}
			};

			list.ForEach((company) =>
			{
				ApplicationContext
					.Container
					.Resolve<IHandler
						<SaveCompanyRediSRequest,
						IWorkerContext<SaveCompanyRediSResponse>>>()
					.Handle(company);
			});
		}
	}
}
