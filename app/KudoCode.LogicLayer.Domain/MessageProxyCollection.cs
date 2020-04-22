using System.Collections.Generic;
using KudoCode.LogicLayer.Dtos;
using KudoCode.LogicLayer.Infrastructure.Dtos.Messages;
using KudoCode.LogicLayer.Infrastructure.Messages;

namespace KudoCode.LogicLayer.Domain
{
	public class MessageProxyCollection : MessageCollection, IMessageCollection
	{
		public IReadOnlyCollection<MessageDto> Messages => base.Messages;
	}
}