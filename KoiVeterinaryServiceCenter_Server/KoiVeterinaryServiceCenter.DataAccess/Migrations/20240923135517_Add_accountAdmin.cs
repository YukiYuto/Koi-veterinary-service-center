using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Add_accountAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35446074-daa5-4973-bf02-82301a5eb327", "DOCTOR", "DOCTOR", "DOCTOR" },
                    { "8fa7c7bb-b4dc-480d-a660-e07a90855d5d", "CUSTOMER", "CUSTOMER", "CUSTOMER" },
                    { "8fa7c7bb-daa5-a660-bf02-82301a5eb327", "ADMIN", "ADMIN", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AvatarUrl", "BirthDate", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "BestZedAndYasuo", 0, "123 Admin St", "https://example.com/avatar.png", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "759826d9-910a-40ea-8c5c-55d8e959457b", "Country", "admin@gmail.com", true, "Admin User", "Male", true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEGuKRsCn7sDfXet3cIY+nMLWxqTbCkzE72cTgavmgkNeYfWqlKkGyM1Ryx4WTh9ogw==", "1234567890", true, "aee3ca2f-c8ca-4829-baec-cfa4dd4ec0f5", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8fa7c7bb-daa5-a660-bf02-82301a5eb327", "BestZedAndYasuo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35446074-daa5-4973-bf02-82301a5eb327");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fa7c7bb-b4dc-480d-a660-e07a90855d5d");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8fa7c7bb-daa5-a660-bf02-82301a5eb327", "BestZedAndYasuo" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fa7c7bb-daa5-a660-bf02-82301a5eb327");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo");
        }
    }
}
