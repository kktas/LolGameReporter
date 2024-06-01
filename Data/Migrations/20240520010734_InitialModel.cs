using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LolGameReporter.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "t_chat",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TelegramChatId = table.Column<long>(type: "bigint", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_account",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tag = table.Column<string>(type: "text", nullable: false),
                    ChatId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_account_t_chat_ChatId",
                        column: x => x.ChatId,
                        principalSchema: "main",
                        principalTable: "t_chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_account_ChatId",
                schema: "main",
                table: "t_account",
                column: "ChatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_account",
                schema: "main");

            migrationBuilder.DropTable(
                name: "t_chat",
                schema: "main");
        }
    }
}
