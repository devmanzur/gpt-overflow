using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPTOverflow.Core.UserManagement.Brokers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user-management");

            migrationBuilder.CreateTable(
                name: "access_permission",
                schema: "user-management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    displayname = table.Column<string>(name: "display_name", type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "admin_user",
                schema: "user-management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emailaddress = table.Column<string>(name: "email_address", type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_admin_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                schema: "user-management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "application_user",
                schema: "user-management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emailaddress = table.Column<string>(name: "email_address", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    suspendeduntil = table.Column<DateTime>(name: "suspended_until", type: "datetime2", nullable: true),
                    roleid = table.Column<Guid>(name: "role_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_application_user_roles_role_id",
                        column: x => x.roleid,
                        principalSchema: "user-management",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_access_permission",
                schema: "user-management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roleid = table.Column<Guid>(name: "role_id", type: "uniqueidentifier", nullable: false),
                    accesspermissionid = table.Column<Guid>(name: "access_permission_id", type: "uniqueidentifier", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(max)", nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_access_permission", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_access_permission_access_permissions_access_permission_id",
                        column: x => x.accesspermissionid,
                        principalSchema: "user-management",
                        principalTable: "access_permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_access_permission_roles_role_id",
                        column: x => x.roleid,
                        principalSchema: "user-management",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_access_permission_name",
                schema: "user-management",
                table: "access_permission",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_admin_user_email_address",
                schema: "user-management",
                table: "admin_user",
                column: "email_address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_application_user_email_address",
                schema: "user-management",
                table: "application_user",
                column: "email_address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_application_user_role_id",
                schema: "user-management",
                table: "application_user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_name",
                schema: "user-management",
                table: "role",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_access_permission_access_permission_id",
                schema: "user-management",
                table: "role_access_permission",
                column: "access_permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_access_permission_role_id",
                schema: "user-management",
                table: "role_access_permission",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_user",
                schema: "user-management");

            migrationBuilder.DropTable(
                name: "application_user",
                schema: "user-management");

            migrationBuilder.DropTable(
                name: "role_access_permission",
                schema: "user-management");

            migrationBuilder.DropTable(
                name: "access_permission",
                schema: "user-management");

            migrationBuilder.DropTable(
                name: "role",
                schema: "user-management");
        }
    }
}
