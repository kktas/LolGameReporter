using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "Champions",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChampionId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
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
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
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
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
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
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_Regions_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "main",
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
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
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Servers_ServerId",
                        column: x => x.ServerId,
                        principalSchema: "main",
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountChat",
                schema: "main",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    ChatId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountChat", x => new { x.AccountId, x.ChatId });
                    table.ForeignKey(
                        name: "FK_AccountChat_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "main",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountChat_Chats_ChatId",
                        column: x => x.ChatId,
                        principalSchema: "main",
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "Regions",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "CreatedByName", "DeletedAt", "DeletedById", "DeletedByName", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4560), 0L, "", null, null, null, "Americas" },
                    { 2, new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4595), 0L, "", null, null, null, "Europe" },
                    { 3, new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4597), 0L, "", null, null, null, "Asia" }
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "Servers",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "CreatedByName", "DeletedAt", "DeletedById", "DeletedByName", "Name", "RegionId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4721), 0L, "", null, null, null, "NA", 1 },
                    { 2, new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4724), 0L, "", null, null, null, "EUW", 2 },
                    { 3, new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4726), 0L, "", null, null, null, "EUNE", 2 },
                    { 4, new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4727), 0L, "", null, null, null, "TR", 2 },
                    { 5, new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4729), 0L, "", null, null, null, "JP", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountChat_ChatId",
                schema: "main",
                table: "AccountChat",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ServerId",
                schema: "main",
                table: "Accounts",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_RegionId",
                schema: "main",
                table: "Servers",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountChat",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Champions",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Chats",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Servers",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Regions",
                schema: "main");
        }
    }
}
