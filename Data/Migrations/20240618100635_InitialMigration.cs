using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                name: "t_region",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_server",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedByName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_server", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_server_t_region_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "main",
                        principalTable: "t_region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_account",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameName = table.Column<string>(type: "text", nullable: false),
                    TagLine = table.Column<string>(type: "text", nullable: false),
                    Puuid = table.Column<string>(type: "text", nullable: false),
                    ServerId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_t_account_t_server_ServerId",
                        column: x => x.ServerId,
                        principalSchema: "main",
                        principalTable: "t_server",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_account_chat",
                schema: "main",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    ChatId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_account_chat", x => new { x.AccountId, x.ChatId });
                    table.ForeignKey(
                        name: "FK_t_account_chat_t_account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "main",
                        principalTable: "t_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_account_chat_t_chat_ChatId",
                        column: x => x.ChatId,
                        principalSchema: "main",
                        principalTable: "t_chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "t_region",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "CreatedByName", "DeletedAt", "DeletedById", "DeletedByName", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 18, 10, 6, 33, 175, DateTimeKind.Utc).AddTicks(8365), 0L, "", null, null, null, "Americas" },
                    { 2, new DateTime(2024, 6, 18, 10, 6, 33, 175, DateTimeKind.Utc).AddTicks(8412), 0L, "", null, null, null, "Europe" },
                    { 3, new DateTime(2024, 6, 18, 10, 6, 33, 175, DateTimeKind.Utc).AddTicks(8415), 0L, "", null, null, null, "Asia" }
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "t_server",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "CreatedByName", "DeletedAt", "DeletedById", "DeletedByName", "Name", "RegionId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 18, 10, 6, 33, 175, DateTimeKind.Utc).AddTicks(8602), 0L, "", null, null, null, "NA", 1 },
                    { 2, new DateTime(2024, 6, 18, 10, 6, 33, 175, DateTimeKind.Utc).AddTicks(8607), 0L, "", null, null, null, "EUW", 2 },
                    { 3, new DateTime(2024, 6, 18, 10, 6, 33, 175, DateTimeKind.Utc).AddTicks(8610), 0L, "", null, null, null, "EUNE", 2 },
                    { 4, new DateTime(2024, 6, 18, 10, 6, 33, 175, DateTimeKind.Utc).AddTicks(8612), 0L, "", null, null, null, "TR", 2 },
                    { 5, new DateTime(2024, 6, 18, 10, 6, 33, 175, DateTimeKind.Utc).AddTicks(8614), 0L, "", null, null, null, "JP", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_account_ServerId",
                schema: "main",
                table: "t_account",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_t_account_chat_ChatId",
                schema: "main",
                table: "t_account_chat",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_t_server_RegionId",
                schema: "main",
                table: "t_server",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_account_chat",
                schema: "main");

            migrationBuilder.DropTable(
                name: "t_account",
                schema: "main");

            migrationBuilder.DropTable(
                name: "t_chat",
                schema: "main");

            migrationBuilder.DropTable(
                name: "t_server",
                schema: "main");

            migrationBuilder.DropTable(
                name: "t_region",
                schema: "main");
        }
    }
}
