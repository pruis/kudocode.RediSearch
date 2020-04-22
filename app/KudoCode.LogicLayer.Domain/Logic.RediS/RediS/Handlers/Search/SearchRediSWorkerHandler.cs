using Autofac;
using AutoMapper;
using KudoCode.LogicLayer.Infrastructure.Execution.Context.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Handlers;
using KudoCode.LogicLayer.Dtos.Companies.Inbound;
using KudoCode.LogicLayer.Dtos.CompanyRediSs.Outbound;
using NRediSearch;
using KudoCode.LogicLayer.Domain.RediSearchClients;
using System.Collections.Generic;
using System;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using System.Dynamic;

namespace KudoCode.LogicLayer.Domain.Logic.CompanyRediSs.Handlers.Search
{
	public class SearchRediSWorkerHandler
		: WorkerHandler<SearchRediSRequest, SearchRediSResponse>
	{
		private readonly IConfiguration _configuration;
		public SearchRediSWorkerHandler(
				IMapper mapper,
				ILifetimeScope scope,
				IConfiguration configuration,
				IWorkerContext<SearchRediSResponse> context)
			: base(mapper, scope, context)
		{
			_configuration = configuration;
		}
		protected override void Execute()
		{
			using (var rediSConnection = new RediSConnection(_configuration["RedisUrl"]))
			{
				//RediSearch Set Client
				rediSConnection.SetClient(Request.IndexName);

				//RediSearch Query
				Query q = new Query(Request.SearchText);
				q.Limit(Request.Offset, Request.Count);
				q.SetWithScores(true);
				var rediSResult = rediSConnection.Client.Search(q).Documents;

				//Cast to final Result
				Context.Result = new SearchRediSResponse();
				rediSResult.ForEach((result) =>
					Context.Result.Add(result.Score, result.GetProperties().DictionaryToDynamic())
				);
			}
		}
	}
	public static class Helpers
	{
		public static dynamic DictionaryToDynamic(this IEnumerable<KeyValuePair<String, RedisValue>> dictionary)
		{
			var response = new ExpandoObject();
			foreach (var prop in dictionary)
				(response as IDictionary<string, object>)[prop.Key] = prop.Value;
			return response;
		}
	}
}
