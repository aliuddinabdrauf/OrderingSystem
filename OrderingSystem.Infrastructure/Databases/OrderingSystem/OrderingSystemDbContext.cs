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
        protected virtual DbSet<TblBase> TblBase {  get; set; }
        protected virtual DbSet<TblBaseSoftDelete> TblBaseSoftDelete {  get; set; }
        public virtual DbSet<TblMenu> TblMenu { get; set; }
        public virtual DbSet<TblOrder> TblOrder { get; set; }
        public virtual DbSet<TblReciept> TblPayment { get; set; }
        public virtual DbSet<TblTable> TblTable { get; set; }
        public virtual DbSet<TblMenuGroup> TblMenuGroup { get; set; }
        public virtual DbSet<TblOrderToReciept> TblOrderToReciept { get; set; }
        public virtual DbSet<TblAuditTrail> TblAuditTrail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //table that implement softdelete, will only include non-flag isdeleted in query result
            modelBuilder.Entity<TblBaseSoftDelete>().HasQueryFilter(x => !x.IsDeleted);

            //map enum
            modelBuilder.HasPostgresEnum<MenuType>();
            modelBuilder.HasPostgresEnum<PaymentType>();
            modelBuilder.HasPostgresEnum<EntityState>();

            modelBuilder.Entity<TblBase>(entity =>
            {
                //entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.UseTpcMappingStrategy();
            });
            modelBuilder.Entity<TblBaseSoftDelete>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.IsDeleted);
                entity.UseTpcMappingStrategy();
            });

            modelBuilder.Entity<TblMenuGroup>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(1000);
            });
            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique().HasFilter("is_deleted = false");
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.HasOne( e=> e.MenuGroup).WithMany(e => e.Menus).HasForeignKey(e => e.MenuGroupId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.Property(e => e.Instruction).HasMaxLength(100);
                entity.HasOne(e => e.Menu).WithMany(e => e.Orders)
                .HasForeignKey(e => e.MenuId)
                .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.OrderToReciept).WithOne(e => e.Order)
                .HasForeignKey<TblOrderToReciept>(e => e.OrderId);
                entity.HasOne(e => e.Table).WithMany(e => e.Orders)
                .HasForeignKey(e => e.TableId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<TblReciept>(entity =>
            {
                entity.HasMany(e => e.OrderToReciepts).WithOne(e => e.Reciept)
                .HasForeignKey(e => e.RecieptId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<TblAuditTrail>(entity =>
            {
                entity.Property(e => e.TableName).HasMaxLength(100);
                entity.Property(e => e.Data).HasMaxLength(int.MaxValue);
            });
        }
    }
}
