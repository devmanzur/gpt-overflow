﻿// <auto-generated />
using System;
using GPTOverflow.Core.UserManagement.Brokers.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GPTOverflow.Core.UserManagement.Brokers.Persistence.Migrations
{
    [DbContext(typeof(UserManagementDbContext))]
    partial class UserManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("user-management")
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GPTOverflow.Core.UserManagement.Models.AccessPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("created_by");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("display_name");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_updated_at");

                    b.Property<string>("LastUpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("last_updated_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_access_permission");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_access_permission_name");

                    b.ToTable("access_permission", "user-management");
                });

            modelBuilder.Entity("GPTOverflow.Core.UserManagement.Models.AdminUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("created_by");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email_address");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_updated_at");

                    b.Property<string>("LastUpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("last_updated_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_admin_user");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasDatabaseName("ix_admin_user_email_address");

                    b.ToTable("admin_user", "user-management");
                });

            modelBuilder.Entity("GPTOverflow.Core.UserManagement.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("created_by");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email_address");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_updated_at");

                    b.Property<string>("LastUpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("last_updated_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.Property<DateTime?>("SuspendedUntil")
                        .HasColumnType("datetime2")
                        .HasColumnName("suspended_until");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_application_user");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasDatabaseName("ix_application_user_email_address");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_application_user_role_id");

                    b.ToTable("application_user", "user-management");
                });

            modelBuilder.Entity("GPTOverflow.Core.UserManagement.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_role_name");

                    b.ToTable("role", "user-management");
                });

            modelBuilder.Entity("GPTOverflow.Core.UserManagement.Models.RoleAccessPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("AccessPermissionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("access_permission_id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_updated_at");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_updated_by");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_role_access_permission");

                    b.HasIndex("AccessPermissionId")
                        .HasDatabaseName("ix_role_access_permission_access_permission_id");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_role_access_permission_role_id");

                    b.ToTable("role_access_permission", "user-management");
                });

            modelBuilder.Entity("GPTOverflow.Core.UserManagement.Models.ApplicationUser", b =>
                {
                    b.HasOne("GPTOverflow.Core.UserManagement.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_application_user_roles_role_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("GPTOverflow.Core.UserManagement.Models.RoleAccessPermission", b =>
                {
                    b.HasOne("GPTOverflow.Core.UserManagement.Models.AccessPermission", "AccessPermission")
                        .WithMany()
                        .HasForeignKey("AccessPermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_access_permission_access_permissions_access_permission_id");

                    b.HasOne("GPTOverflow.Core.UserManagement.Models.Role", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_access_permission_roles_role_id");

                    b.Navigation("AccessPermission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("GPTOverflow.Core.UserManagement.Models.Role", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
