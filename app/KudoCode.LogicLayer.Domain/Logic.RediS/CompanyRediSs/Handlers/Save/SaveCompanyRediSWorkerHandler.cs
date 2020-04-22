using Autofac;
using AutoMapper;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Inbound;
using System.Collections.Generic;
using StackExchange.Redis;
using KudoCode.LogicLayer.Domain.RediSearchClients;
using Microsoft.Extensions.Configuration;
using NRediSearch;

namespace KudoCode.LogicLayer.Domain.Logic.CompanyRediSs.Handlers.Save
{
	public class SaveCompanyRediSWorkerHandler
		: WorkerHandler<SaveCompanyRediSRequest, SaveCompanyRediSResponse>
	{
		public SaveCompanyRediSWorkerHandler(
				IMapper mapper,
	 			ILifetimeScope scope,
				IConfiguration configuration,
				IWorkerContext<SaveCompanyRediSResponse> context)
			: base(mapper, scope, context)
		{
			_configuration = configuration;
		}

		private IConfiguration _configuration { get; }

		protected override void Execute()
		{
			using (var rediSConnection = new RediSConnection(_configuration["RedisUrl"]))
			{
				//Set all local variables 
				var indexName = "GetCompany";
				var docId = $"{indexName}-{Request.Id}";

				//RediSearch Set Client
				rediSConnection.SetClient(
						indexName: indexName,
						textFields:
							new List<(string, int)> { ("Name", 9), ("Description", 2) }
					);

				//RediSearch Add or Replace 
				rediSConnection.Client.AddDocument(
					new Document(docId, Request.AsDictionary()),
					new AddOptions()
					{
						ReplacePolicy = AddOptions.ReplacementPolicy.Full
					});
			}

			// Set the result
			if (Context.Result == null)
				Context.Result = new SaveCompanyRediSResponse();
		}

	}

	public static class Helpers
	{
		public static Dictionary<string, RedisValue> AsDictionary(this object me)
		{
			var result = new Dictionary<string, RedisValue>();
			foreach (var property in me.GetType().GetProperties())
				result.Add(property.Name, property.GetValue(me)?.ToString());
			return result;
		}
	}
}
