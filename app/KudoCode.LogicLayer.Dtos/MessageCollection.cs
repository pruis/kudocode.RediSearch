using System.Collections.Generic;
using KudoCode.LogicLayer.Infrastructure.Dtos.Messages;

namespace KudoCode.LogicLayer.Dtos
{
	public class MessageCollection 
	{
		public IReadOnlyCollection<MessageDto> Messages => new List<MessageDto>()
		{
			new MessageDto("E1", "{0}", MessageDtoType.Error),
			new MessageDto("E2", "Internal Error please contact support with the following reference. {0}"
				, MessageDtoType.Error),
			new MessageDto("E3", "Authorization Failed", MessageDtoType.Error),
			new MessageDto("W3", "Authorization token not provided", MessageDtoType.Warning),
			new MessageDto("E4","Not found: {0}",MessageDtoType.Error),
			new MessageDto("E5","**E5 IS NOT USED {0}**",MessageDtoType.Error),
			new MessageDto("E6","Input validation: {0}",MessageDtoType.Error),
			new MessageDto("W6","Input validation: {0}",MessageDtoType.Warning),
		};
	}
}