using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Modify_DB_DSService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DoctorSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "DoctorSchedules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DoctorSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DoctorSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "DoctorSchedules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3392061a-7960-475a-876a-d9dc207f75e3", "AQAAAAIAAYagAAAAEDXOnRp+tYesIoXLP8Cq3PEylMCeloZ5NPcEwrmeGRYXaBlL285JGAMDJuYUniX0uA==", "fb2f9c6d-b738-4e21-9ea0-a77f01f22065" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "DoctorSchedules");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bbba0dc-3b08-4d1b-8830-6ce3c08b2a6e", "AQAAAAIAAYagAAAAEGphzjIQnVUz3ZvauKX8g6jbuKV+qdhm23rugUY2GYhdJkIBIq2VyxE4e7B4qqZd5w==", "960eb1e4-2428-498e-829e-a9e0105f2b36" });
        }
    }
}
