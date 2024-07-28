using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChampions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "main",
                table: "Champions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                schema: "main",
                table: "Champions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                schema: "main",
                table: "Champions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "main",
                table: "Champions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeletedById",
                schema: "main",
                table: "Champions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByName",
                schema: "main",
                table: "Champions",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 22, 8, 57, 29, 60, DateTimeKind.Utc).AddTicks(243));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 22, 8, 57, 29, 60, DateTimeKind.Utc).AddTicks(276));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 22, 8, 57, 29, 60, DateTimeKind.Utc).AddTicks(278));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 22, 8, 57, 29, 60, DateTimeKind.Utc).AddTicks(398));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 22, 8, 57, 29, 60, DateTimeKind.Utc).AddTicks(401));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Servers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 22, 8, 57, 29, 60, DateTimeKind.Utc).AddTicks(403));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Servers",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 22, 8, 57, 29, 60, DateTimeKind.Utc).AddTicks(405));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Servers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 22, 8, 57, 29, 60, DateTimeKind.Utc).AddTicks(406));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "main",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "main",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                schema: "main",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "main",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                schema: "main",
                table: "Champions");

            migrationBuilder.DropColumn(
                name: "DeletedByName",
                schema: "main",
                table: "Champions");

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4560));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4595));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4597));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4721));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4724));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Servers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4726));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Servers",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4727));

            migrationBuilder.UpdateData(
                schema: "main",
                table: "Servers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 19, 18, 10, 40, 753, DateTimeKind.Utc).AddTicks(4729));
        }
    }
}
