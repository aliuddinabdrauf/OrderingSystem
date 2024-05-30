using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace OrderingSystem.Infrastructure.Databases.OrderingSystem
{
    public class OrderingSystemDbContext: IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public OrderingSystemDbContext(DbContextOptions<OrderingSystemDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //table that implement softdelete, will only include non-flag isdeleted in query result
            modelBuilder.Entity<TblBaseSoftDelete>().HasQueryFilter(x => !x.IsDeleted);

            //map enum
            modelBuilder.HasPostgresEnum<MenuType>();

            modelBuilder.Entity<TblBase>(entity =>
            {
                //entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.TimestampCreated).ValueGeneratedOnAdd();
                entity.Property(e => e.TimestampUpdated).ValueGeneratedOnAdd().ValueGeneratedOnUpdate();
                entity.UseTpcMappingStrategy();
            });
            modelBuilder.Entity<TblBaseSoftDelete>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted);
                entity.UseTpcMappingStrategy();
            });
        }
    }
}
