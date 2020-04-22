using System;
using Autofac;
using KudoCode.LogicLayer.Infrastructure;
using KudoCode.LogicLayer.Infrastructure.Domain.Logic.ApplicationUsers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace KudoCode.Web.Api
{
	public static class MockDataBase
	{
		internal static void InitializeDataBase()
		{
			using (var scope = ApplicationContext.Container.BeginLifetimeScope("ExecutionPipeline"))
			{
				using (var context = scope.Resolve<DbContext>())
				{
					context.Database.EnsureDeleted();

					if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
						return;

					context.Database.EnsureDeleted();
					context.Database.EnsureCreated();
					//context.Database.Migrate();
					context.SaveChanges();
					var organization = new EntityOrganization()
						{Name = "Default", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now};

					var adminRole = new AuthorizationRole()
						{Id = 1, Name = "Administrator", EntityOrganization = organization};
					var agentRole = new AuthorizationRole() {Id = 2, Name = "Agent", EntityOrganization = organization};
					var advisoryRole = new AuthorizationRole()
						{Id = 3, Name = "Advisor", EntityOrganization = organization};
					var workflowRole = new AuthorizationRole()
						{Id = 4, Name = "Workflow", EntityOrganization = organization};

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
					var schedulerFlowUser = new ApplicationUser()
					{
						Name = "Scheduler", Email = "scheduler@flow.com",
						Password = "\u001f\r?????\u001e\u0016kt??#?\u0014?F??\u0014\u0018S?Ar??36??",
						CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
					};


					var authorizationGroupA = new AuthorizationGroup()
						{Id = 1, Name = "Group A", EntityOrganization = organization};
					var authorizationGroupB = new AuthorizationGroup()
						{Id = 2, Name = "Group B", EntityOrganization = organization};
					context.SaveChanges();


					context.SaveChanges();


					applicationUser.AddEntityOrganization(organization);
					applicationUser.ActiveEntityOrganizationId = 1;
					applicationUser.AddAuthorizationGroup(authorizationGroupA);
					applicationUser.AuthorizationRole = adminRole;

					context.Add(organization);
					context.Add(adminRole);
					context.Add(advisoryRole);
					context.Add(agentRole);
					context.Add(workflowRole);
					context.Add(applicationUser);
					context.Add(workflowUser);
					context.Add(schedulerFlowUser);
					context.Add(authorizationGroupA);
					//context.Add(authorizationGroupB);

					for (int i = 0; i < 1000; i++)
					{
						var user = new ApplicationUser()
						{
							Name = "Default", Email = $"testB@test{i}.com",
							Password = "\u001f\r?????\u001e\u0016kt??#?\u0014?F??\u0014\u0018S?Ar??36??",
							CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now
						};
						user.AddEntityOrganization(organization);
						user.ActiveEntityOrganizationId = 1;
						user.AddAuthorizationGroup(authorizationGroupA);
						user.AuthorizationRole = adminRole;
						context.Add(user);
					}

				
					context.SaveChanges();
				}
			}
		}
	}
}