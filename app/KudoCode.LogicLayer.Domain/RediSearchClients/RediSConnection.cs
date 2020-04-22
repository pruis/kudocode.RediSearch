using System.Collections.Generic;
using StackExchange.Redis;
using NRediSearch;
using static NRediSearch.Client;
using System;

namespace KudoCode.LogicLayer.Domain.RediSearchClients
{
	public sealed class RediSConnection : IDisposable
	{
		private ConnectionMultiplexer _redis;
		public IDatabase Database { get; private set; }
		private string _url { get; }

		public Client Client;

		public RediSConnection(string url)
		{
			_url = url;
		}

		public void Dispose()
		{
			Client = null;
			Database = null;
			if (_redis != null)
				_redis.Close();
		}

		public RediSConnection SetClient(
			string indexName, List<(string, int)> textFields = null)
		{
			if (_redis == null)
				_redis = ConnectionMultiplexer.Connect(_url);

			if (Database == null)
				Database = _redis.GetDatabase();

			Client = new Client(indexName, Database);

			if (textFields == null)
				return this;

			//attempt to create index if fields are provided 
			try
			{
				Schema sch = new Schema();
				textFields.ForEach((x) => sch.AddTextField(x.Item1, x.Item2));
				Client.CreateIndex(sch, new ConfiguredIndexOptions());
			}
			catch (Exception e)
			{
				//Ignore Exception 'Index already exists' 
				if (!e.Message.Contains("Index already exists"))
					throw;
			}

			return this;
		}

	}
}
