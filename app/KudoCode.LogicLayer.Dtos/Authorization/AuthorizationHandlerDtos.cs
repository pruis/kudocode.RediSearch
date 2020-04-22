using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers;

namespace KudoCode.LogicLayer.Dtos.Authorization
{
	public static class AuthorizationHandlerDtos
	{

		public static class Roles
		{
			public static AuthorizationRoleDto Administrator => new AuthorizationRoleDto()
			{ Id = 1, Name = "Administrator" };
			public static AuthorizationRoleDto Advisor => new AuthorizationRoleDto()
			{ Id = 2, Name = "Advisor" };
			public static AuthorizationRoleDto Agent => new AuthorizationRoleDto()
			{ Id = 3, Name = "Advisor" };
			public static AuthorizationRoleDto Workflow => new AuthorizationRoleDto()
			{ Id = 3, Name = "Workflow" };

		}
		public static class Groups
		{
			public static AuthorizationGroupDto GroupA => new AuthorizationGroupDto()
			{
				Id = 1,
				EntityOrganizationId = 1,
				Name = "Group A"
			};
			public static AuthorizationGroupDto GroupB => new AuthorizationGroupDto()
			{
				Id = 2,
				EntityOrganizationId = 1,
				Name = "Group B"
			};
		}

		public static AuthorizationHandlerDto Get
		{
			get
			{
				var dto = new AuthorizationHandlerDto();
				dto.Groups.Add(Groups.GroupA);
				dto.Groups.Add(Groups.GroupB);
				dto.Role = Roles.Administrator;
				return dto;
			}
		}
	}
}