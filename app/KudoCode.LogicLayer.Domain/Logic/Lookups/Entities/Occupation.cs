using System.ComponentModel.DataAnnotations.Schema;
using KudoCode.LogicLayer.Infrastructure.Domain.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Domain.Logic.ApplicationUsers.Entities;
using KudoCode.LogicLayer.Infrastructure.Domain.Logic.ApplicationUsers.Entities.Interfaces;

namespace KudoCode.LogicLayer.Domain.Logic.Lookups.Entities
{
	public class Occupation : IEntity, IBelongToOrganization, ILookup
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		public string Description { get; set; }
		public int EntityOrganizationId { get; set; }
		public EntityOrganization EntityOrganization { get; set; }
	}
}