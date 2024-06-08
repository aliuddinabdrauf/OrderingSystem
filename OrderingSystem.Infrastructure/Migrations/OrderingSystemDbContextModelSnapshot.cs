﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;

#nullable disable

namespace OrderingSystem.Infrastructure.Migrations
{
    [DbContext(typeof(OrderingSystemDbContext))]
    partial class OrderingSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "entity_state", new[] { "detached", "unchanged", "deleted", "modified", "added" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "menu_type", new[] { "others", "main_course", "drinks", "dessert" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "payment_type", new[] { "cash", "qr_scan", "credit_card" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_role_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_role_claims_role_id");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_users");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_user_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_claims_user_id");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_asp_net_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_logins_user_id");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_asp_net_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_user_roles_role_id");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_asp_net_user_tokens");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblAuditTrail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<EntityState>("Action")
                        .HasColumnType("entity_state")
                        .HasColumnName("action");

                    b.Property<DateTimeOffset>("ActionTimestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("action_timestamp");

                    b.Property<Guid>("ActorId")
                        .HasColumnType("uuid")
                        .HasColumnName("actor_id");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("text")
                        .HasColumnName("data");

                    b.Property<Guid>("TableId")
                        .HasColumnType("uuid")
                        .HasColumnName("table_id");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("table_name");

                    b.HasKey("Id")
                        .HasName("pk_tbl_audit_trail");

                    b.ToTable("tbl_audit_trail", (string)null);
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("TimestampCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp_created");

                    b.Property<DateTimeOffset>("TimestampUpdated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp_updated");

                    b.HasKey("Id");

                    b.ToTable("tbl_base", (string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblBaseSoftDelete", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTimeOffset>("TimestampCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp_created");

                    b.Property<DateTimeOffset?>("TimestampDeleted")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp_deleted");

                    b.Property<DateTimeOffset>("TimestampUpdated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp_updated");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("tbl_base_soft_delete", (string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblFile", b =>
                {
                    b.HasBaseType("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblBase");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasMaxLength(10000000)
                        .HasColumnType("bytea")
                        .HasColumnName("data");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("extension");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint")
                        .HasColumnName("file_size");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.ToTable("tbl_file", (string)null);
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblMenuImage", b =>
                {
                    b.HasBaseType("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblBase");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid")
                        .HasColumnName("file_id");

                    b.Property<Guid>("MenuId")
                        .HasColumnType("uuid")
                        .HasColumnName("menu_id");

                    b.Property<int>("Order")
                        .HasColumnType("integer")
                        .HasColumnName("order");

                    b.HasIndex("FileId")
                        .HasDatabaseName("ix_tbl_menu_image_file_id");

                    b.HasIndex("MenuId")
                        .HasDatabaseName("ix_tbl_menu_image_menu_id");

                    b.ToTable("tbl_menu_image", (string)null);
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblOrderToReciept", b =>
                {
                    b.HasBaseType("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblBase");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<Guid>("RecieptId")
                        .HasColumnType("uuid")
                        .HasColumnName("reciept_id");

                    b.HasIndex("OrderId")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_order_to_reciept_order_id");

                    b.HasIndex("RecieptId")
                        .HasDatabaseName("ix_tbl_order_to_reciept_reciept_id");

                    b.ToTable("tbl_order_to_reciept", (string)null);
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblMenu", b =>
                {
                    b.HasBaseType("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblBaseSoftDelete");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<Guid?>("MenuGroupId")
                        .HasColumnType("uuid")
                        .HasColumnName("menu_group_id");

                    b.Property<MenuType>("MenuType")
                        .HasColumnType("menu_type")
                        .HasColumnName("menu_type");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.HasIndex("MenuGroupId")
                        .HasDatabaseName("ix_tbl_menu_menu_group_id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_menu_name")
                        .HasFilter("is_deleted = false");

                    b.ToTable("tbl_menu", (string)null);
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblMenuGroup", b =>
                {
                    b.HasBaseType("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblBaseSoftDelete");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_menu_group_name")
                        .HasFilter("is_deleted = false");

                    b.ToTable("tbl_menu_group", (string)null);
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblOrder", b =>
                {
                    b.HasBaseType("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblBaseSoftDelete");

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("instruction");

                    b.Property<Guid>("MenuId")
                        .HasColumnType("uuid")
                        .HasColumnName("menu_id");

                    b.Property<Guid>("TableId")
                        .HasColumnType("uuid")
                        .HasColumnName("table_id");

                    b.Property<int>("Total")
                        .HasColumnType("integer")
                        .HasColumnName("total");

                    b.HasIndex("MenuId")
                        .HasDatabaseName("ix_tbl_order_menu_id");

                    b.HasIndex("TableId")
                        .HasDatabaseName("ix_tbl_order_table_id");

                    b.ToTable("tbl_order", (string)null);
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblReciept", b =>
                {
                    b.HasBaseType("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblBaseSoftDelete");

                    b.Property<PaymentType>("PaymentType")
                        .HasColumnType("payment_type")
                        .HasColumnName("payment_type");

                    b.Property<double>("Total")
                        .HasColumnType("double precision")
                        .HasColumnName("total");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("transaction_id");

                    b.ToTable("tbl_payment", (string)null);
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblTable", b =>
                {
                    b.HasBaseType("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblBaseSoftDelete");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.ToTable("tbl_table", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_role_claims_asp_net_roles_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_claims_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_logins_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_roles_role_id");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_tokens_asp_net_users_user_id");
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblMenuImage", b =>
                {
                    b.HasOne("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblFile", "Image")
                        .WithMany("MenuImages")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_menu_image_tbl_file_file_id");

                    b.HasOne("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblMenu", "Menu")
                        .WithMany("MenuImages")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_menu_image_tbl_menu_menu_id");

                    b.Navigation("Image");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblOrderToReciept", b =>
                {
                    b.HasOne("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblOrder", "Order")
                        .WithOne("OrderToReciept")
                        .HasForeignKey("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblOrderToReciept", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_order_to_reciept_tbl_order_order_id");

                    b.HasOne("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblReciept", "Reciept")
                        .WithMany("OrderToReciepts")
                        .HasForeignKey("RecieptId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_order_to_reciept_tbl_payment_reciept_id");

                    b.Navigation("Order");

                    b.Navigation("Reciept");
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblMenu", b =>
                {
                    b.HasOne("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblMenuGroup", "MenuGroup")
                        .WithMany("Menus")
                        .HasForeignKey("MenuGroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("fk_tbl_menu_tbl_menu_group_menu_group_id");

                    b.Navigation("MenuGroup");
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblOrder", b =>
                {
                    b.HasOne("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblMenu", "Menu")
                        .WithMany("Orders")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_order_tbl_menu_menu_id");

                    b.HasOne("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblTable", "Table")
                        .WithMany("Orders")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_order_tbl_table_table_id");

                    b.Navigation("Menu");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblFile", b =>
                {
                    b.Navigation("MenuImages");
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblMenu", b =>
                {
                    b.Navigation("MenuImages");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblMenuGroup", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblOrder", b =>
                {
                    b.Navigation("OrderToReciept")
                        .IsRequired();
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblReciept", b =>
                {
                    b.Navigation("OrderToReciepts");
                });

            modelBuilder.Entity("OrderingSystem.Infrastructure.Databases.OrderingSystem.TblTable", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
