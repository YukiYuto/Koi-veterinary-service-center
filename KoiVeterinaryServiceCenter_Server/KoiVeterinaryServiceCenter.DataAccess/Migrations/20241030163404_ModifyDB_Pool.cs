using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDB_Pool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("760bc747-f5d3-40ea-ae00-aa0855e17aac"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("bc33ecfa-4ede-4a21-a2c0-4e3dca231996"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("bec16645-d1fd-46d7-8903-90cffdfecfb5"));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Pets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PetUrl",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Pets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PetDiseases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "PetDiseases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PetDiseases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PetDiseases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "PetDiseases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Diseases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medication",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Diseases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Diseases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    PoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    size = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.PoolId);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46b5b3cd-9917-4e57-8971-58585ed50dff", "AQAAAAIAAYagAAAAELcifF0b9vhcZRgu4dkfZWjJIu28j118+iunowJp2ULg7COklJr7q+jJ0DFSb92qSw==", "c72efcf8-f2bd-4834-ad66-9f288193e332" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("395d2dcb-e52b-4841-9ddd-b3469160a129"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("4d192307-5ccc-46d5-9625-265fcd0d1a05"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null },
                    { new Guid("6c011e86-b136-4d81-8bc3-1e93770e78db"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Reset Your Password", "ForgotPasswordEmail", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CustomerId",
                table: "Notifications",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("395d2dcb-e52b-4841-9ddd-b3469160a129"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("4d192307-5ccc-46d5-9625-265fcd0d1a05"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("6c011e86-b136-4d81-8bc3-1e93770e78db"));

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PetUrl",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PetDiseases");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "PetDiseases");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PetDiseases");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PetDiseases");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "PetDiseases");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Medication",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Diseases");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32e2b56e-456e-4a7e-9f5c-857a801529c1", "AQAAAAIAAYagAAAAEPdOEK5MA0BzRGj16Q6JUJex8JnZJuIsT6IzTXWmuxu7zRQ/NfEMIv779cT60RXfyg==", "df96ba3d-d1ed-41b7-ac4e-0d4195f0caeb" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("760bc747-f5d3-40ea-ae00-aa0855e17aac"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("bc33ecfa-4ede-4a21-a2c0-4e3dca231996"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Reset Your Password", "ForgotPasswordEmail", null, null },
                    { new Guid("bec16645-d1fd-46d7-8903-90cffdfecfb5"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null }
                });
        }
    }
}
