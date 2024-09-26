using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Modify_DB_Slot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Slots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Slots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Slots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Slots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Slots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4dfe8140-ee0d-4246-9ea2-81f76439146c", "AQAAAAIAAYagAAAAEDNrz1tAF/YLGF+pYAZZwgupNAYruScaykv9ZT2g1wJAy3gh31o4TUQOLAa3CL2GJg==", "074bdb71-b605-4704-9c1e-c1b0550d6e09" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Slots");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "759826d9-910a-40ea-8c5c-55d8e959457b", "AQAAAAIAAYagAAAAEGuKRsCn7sDfXet3cIY+nMLWxqTbCkzE72cTgavmgkNeYfWqlKkGyM1Ryx4WTh9ogw==", "aee3ca2f-c8ca-4829-baec-cfa4dd4ec0f5" });
        }
    }
}
