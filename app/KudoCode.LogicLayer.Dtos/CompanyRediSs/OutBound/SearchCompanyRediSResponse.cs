using KudoCode.LogicLayer.Dtos.Companies.Outbound;
using System;
using System.Collections.Generic;

namespace KudoCode.LogicLayer.Dtos.Companies.Inbound
{
	public class SearchRediSResponse
	{
		public SearchRediSResponse()
		{
			List = new List<RediSResult>();
		}
		public List<RediSResult> List { get; set; }

		public void Add(Double score, dynamic data)
		{
			if (List == null)
				List = new List<RediSResult>();

			List.Add(new RediSResult()
			{
				Score = score,
				Data = data
			});
		}

	}

	public class RediSResult
	{
		public Double Score { get; set; }
		public dynamic Data { get; set; }

	}

}
