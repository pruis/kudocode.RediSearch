using System;
using System.Linq;
using KudoCode.LogicLayer.Infrastructure.Domain.Interfaces;
using KudoCode.LogicLayer.Infrastructure.Domain.Logic.ApplicationUsers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

//https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework60.html
//https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core.html
//		//CREATE TABLE `__EFMigrationsHistory` ( `MigrationId` nvarchar(150) NOT NULL, `ProductVersion` nvarchar(32) NOT NULL, PRIMARY KEY (`MigrationId`) );

namespace KudoCode.LogicLayer.Domain.Repository
{
    public interface IDataContext
    {
    }

    public class DataContext : DbContext, IDataContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration) //: base("name=MyDbConnection")
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var scanforEntity = typeof(IEntity);
            var scanforMapEntity = typeof(IMapEntity);
            var scanforLookup = typeof(ILookup);

            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(a => a.GetTypes())
                .Where(t => scanforEntity.IsAssignableFrom(t)
                            || scanforLookup.IsAssignableFrom(t)
                            || scanforMapEntity.IsAssignableFrom(t));

            foreach (var type in types)
            {
                if (modelBuilder.Model.FindEntityType(type) != null)
                    continue;
                if (type.IsAbstract)
                    continue;

                modelBuilder.Model.AddEntityType(type);
            }

            modelBuilder.Entity<AuthorizationGroup>().Property(t => t.Id).ValueGeneratedNever();
            modelBuilder.Entity<AuthorizationRole>().Property(t => t.Id).ValueGeneratedNever();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                //property.Relational().ColumnType = "decimal(18, 9)";
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => !p.IsPrimaryKey() && !p.IsForeignKey() && p.ClrType == typeof(int)))
            {
                //property.Relational().DefaultValue = "0";
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _configuration.GetSection("ConnectionStrings")["ConnectionLocalSql"],
                builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(120), null); });
        }
    }
}