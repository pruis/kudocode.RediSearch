using System;
using Autofac;
using KudoCode.LogicLayer.Infrastructure.Domain.Logic.ApplicationUsers.Entities;
using KudoCode.LogicLayer.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KudoCode.Test.Unit.InMemoryDataBase
{
	public static class SeedDataBase
	{
		public static void DropAndCreate()
		{
			using (var scope = ApplicationContext.Container.BeginLifetimeScope())
			using (var context = scope.Resolve<DbContext>())
			{
				context.Database.EnsureDeleted();
				context.Database.EnsureCreated();
			}
		}

		public static void Drop()
		{
			//if (ApplicationContext.Container != null)
			using (var scope = ApplicationContext.Container.BeginLifetimeScope())
			using (var context = scope.Resolve<DbContext>())
				context.Database.EnsureCreated();
		}

		public static void InitializeDataBase()
		{
			using (var scope = ApplicationContext.Container.BeginLifetimeScope())
			{
				using (var context = scope.Resolve<DbContext>())
				{
					//if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
					//	return;

					//context.Database.EnsureDeleted();
					//context.Database.EnsureCreated();

					var organization = new EntityOrganization()
						{Name = "Default", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now};

					var adminRole = new AuthorizationRole()
						{Id = 1, Name = "Administrator", EntityOrganization = organization};
					var agentRole = new AuthorizationRole() {Id = 2, Name = "Agent", EntityOrganization = organization};
					var advisorRole = new AuthorizationRole()
						{Id = 3, Name = "Advisor", EntityOrganization = organization};
					var workflowRole = new AuthorizationRole()
						{Id = 4, Name = "Workflow", EntityOrganization = organization};

					var authorizationGroupA = new AuthorizationGroup()
						{Id = 1, Name = "Group A", EntityOrganization = organization};
					var authorizationGroupB = new AuthorizationGroup()
						{Id = 2, Name = "Group B", EntityOrganization = organization};

					var applicationUser = new ApplicationUser()
					{
						Name = "Default", Email = "testB@testC.com",
						Password = "\u001f\r?????\u001e\u0016kt??#?\u0014?F??\u0014\u0018S?Ar??36??",
						CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
					};
					var workflowUser = new ApplicationUser()
					{
						Name = "Workflow", Email = "work@flow.com",
						Password = "\u001f\r?????\u001e\u0016kt??#?\u0014?F??\u0014\u0018S?Ar??36??",
						CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
					};

					applicationUser.AddEntityOrganization(organization);
					applicationUser.ActiveEntityOrganizationId = 1;
					applicationUser.AddAuthorizationGroup(authorizationGroupA);
					applicationUser.AuthorizationRole = adminRole;
					context.Add(organization);
					context.Add(applicationUser);
					context.Add(workflowUser);
					context.Add(authorizationGroupA);
					context.Add(authorizationGroupB);

					context.Add(agentRole);
					context.Add(advisorRole);
					context.Add(workflowRole);

					context.SaveChanges();
				}
			}
		}
	}
}