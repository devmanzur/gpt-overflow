using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPTOverflow.Core.StackExchange.Brokers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "stack_exchange");

            migrationBuilder.CreateTable(
                name: "account",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reputation = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    issoftdeleted = table.Column<bool>(name: "is_soft_deleted", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "badge",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_badge", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flag",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flagreasondescription = table.Column<string>(name: "flag_reason_description", type: "nvarchar(max)", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    accountid = table.Column<Guid>(name: "account_id", type: "uniqueidentifier", nullable: true),
                    questionid = table.Column<Guid>(name: "question_id", type: "uniqueidentifier", nullable: true),
                    answerid = table.Column<Guid>(name: "answer_id", type: "uniqueidentifier", nullable: true),
                    commentid = table.Column<Guid>(name: "comment_id", type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_flag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountid = table.Column<Guid>(name: "account_id", type: "uniqueidentifier", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.id);
                    table.ForeignKey(
                        name: "fk_comment_account_account_id",
                        column: x => x.accountid,
                        principalSchema: "stack_exchange",
                        principalTable: "account",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "question",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountid = table.Column<Guid>(name: "account_id", type: "uniqueidentifier", maxLength: 500, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    closingremark = table.Column<string>(name: "closing_remark", type: "nvarchar(max)", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    issoftdeleted = table.Column<bool>(name: "is_soft_deleted", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_question", x => x.id);
                    table.ForeignKey(
                        name: "fk_question_account_account_id",
                        column: x => x.accountid,
                        principalSchema: "stack_exchange",
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vote",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    accountid = table.Column<Guid>(name: "account_id", type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    answerid = table.Column<Guid>(name: "answer_id", type: "uniqueidentifier", nullable: true),
                    questionid = table.Column<Guid>(name: "question_id", type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vote", x => x.id);
                    table.ForeignKey(
                        name: "fk_vote_account_account_id",
                        column: x => x.accountid,
                        principalSchema: "stack_exchange",
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account_badge",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    accountid = table.Column<Guid>(name: "account_id", type: "uniqueidentifier", nullable: false),
                    badgeid = table.Column<Guid>(name: "badge_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_badge", x => x.id);
                    table.ForeignKey(
                        name: "fk_account_badge_accounts_account_id",
                        column: x => x.accountid,
                        principalSchema: "stack_exchange",
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_account_badge_badges_badge_id",
                        column: x => x.badgeid,
                        principalSchema: "stack_exchange",
                        principalTable: "badge",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answer",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "uniqueidentifier", nullable: false),
                    accountid = table.Column<Guid>(name: "account_id", type: "uniqueidentifier", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    accepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_answer", x => x.id);
                    table.ForeignKey(
                        name: "fk_answer_accounts_account_id",
                        column: x => x.accountid,
                        principalSchema: "stack_exchange",
                        principalTable: "account",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_answer_questions_question_id",
                        column: x => x.questionid,
                        principalSchema: "stack_exchange",
                        principalTable: "question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "question_comment",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question_comment", x => x.id);
                    table.ForeignKey(
                        name: "fk_question_comment_comment_id",
                        column: x => x.id,
                        principalSchema: "stack_exchange",
                        principalTable: "Comment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_question_comment_questions_question_id",
                        column: x => x.questionid,
                        principalSchema: "stack_exchange",
                        principalTable: "question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "question_tag",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "uniqueidentifier", nullable: false),
                    tagid = table.Column<Guid>(name: "tag_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_question_tag", x => x.id);
                    table.ForeignKey(
                        name: "fk_question_tag_questions_question_id",
                        column: x => x.questionid,
                        principalSchema: "stack_exchange",
                        principalTable: "question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_question_tag_tags_tag_id",
                        column: x => x.tagid,
                        principalSchema: "stack_exchange",
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "view",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    accountid = table.Column<Guid>(name: "account_id", type: "uniqueidentifier", nullable: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "uniqueidentifier", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastupdatedat = table.Column<DateTime>(name: "last_updated_at", type: "datetime2", nullable: true),
                    lastupdatedby = table.Column<string>(name: "last_updated_by", type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_view", x => x.id);
                    table.ForeignKey(
                        name: "fk_view_questions_question_id",
                        column: x => x.questionid,
                        principalSchema: "stack_exchange",
                        principalTable: "question",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answer_comment",
                schema: "stack_exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    answerid = table.Column<Guid>(name: "answer_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer_comment", x => x.id);
                    table.ForeignKey(
                        name: "fk_answer_comment_answer_answer_id",
                        column: x => x.answerid,
                        principalSchema: "stack_exchange",
                        principalTable: "answer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_answer_comment_comment_id",
                        column: x => x.id,
                        principalSchema: "stack_exchange",
                        principalTable: "Comment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_account_is_soft_deleted",
                schema: "stack_exchange",
                table: "account",
                column: "is_soft_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_account_username",
                schema: "stack_exchange",
                table: "account",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_account_badge_account_id",
                schema: "stack_exchange",
                table: "account_badge",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_account_badge_badge_id",
                schema: "stack_exchange",
                table: "account_badge",
                column: "badge_id");

            migrationBuilder.CreateIndex(
                name: "ix_answer_account_id",
                schema: "stack_exchange",
                table: "answer",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_answer_question_id",
                schema: "stack_exchange",
                table: "answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_answer_comment_answer_id",
                schema: "stack_exchange",
                table: "answer_comment",
                column: "answer_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_account_id",
                schema: "stack_exchange",
                table: "Comment",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_question_account_id",
                schema: "stack_exchange",
                table: "question",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_question_is_soft_deleted",
                schema: "stack_exchange",
                table: "question",
                column: "is_soft_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_question_comment_question_id",
                schema: "stack_exchange",
                table: "question_comment",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_question_tag_question_id",
                schema: "stack_exchange",
                table: "question_tag",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_question_tag_tag_id",
                schema: "stack_exchange",
                table: "question_tag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_view_question_id",
                schema: "stack_exchange",
                table: "view",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_vote_account_id",
                schema: "stack_exchange",
                table: "vote",
                column: "account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_badge",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "answer_comment",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "flag",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "question_comment",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "question_tag",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "view",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "vote",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "badge",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "answer",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "Comment",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "tag",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "question",
                schema: "stack_exchange");

            migrationBuilder.DropTable(
                name: "account",
                schema: "stack_exchange");
        }
    }
}
