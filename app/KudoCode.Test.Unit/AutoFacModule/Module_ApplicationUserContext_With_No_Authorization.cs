using System;
using Autofac;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers;
using KudoCode.LogicLayer.Infrastructure.Dtos.ApplicationUsers.Interface;
using KudoCode.LogicLayer.Infrastructure.Dtos.Tokens;

namespace KudoCode.Test.Unit.AutoFacModule
{
	public class Module_ApplicationUserContext_With_No_Authorization : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterInstance(new ApplicationUserContext()
			{
				ActiveEntityOrganizationId = 1,
				Id = 1,
				Email = "test@test.com",
				Token = new TokenDto() {Value = "TESTTOKEN", ValidTo = DateTime.Now.AddDays(2)}
				//AuthorizationRole = AuthorizationHandlerDtos.Get().Role,
				//AuthorizationGroups = AuthorizationHandlerDtos.Get().Groups
			}).As<IApplicationUserContext>().SingleInstance();
		}
	}
}
