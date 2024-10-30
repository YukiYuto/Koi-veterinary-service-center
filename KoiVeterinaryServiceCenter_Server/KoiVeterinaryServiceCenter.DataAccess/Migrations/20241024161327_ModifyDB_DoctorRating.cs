using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDB_DoctorRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("1a1815b9-5e42-4b32-ba0d-bad5b1e9b2d9"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("b0bc1703-2244-42dd-977d-0341cf7cad71"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("d971398c-cb8b-4d99-be81-cadeb0324d37"));

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "DoctorRatings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7586887e-2ae9-4c47-a7bd-981db0c60e8d", "AQAAAAIAAYagAAAAEETM//NTtbjY9R5COt42pU3TsoQa+jGAbe5yOk2yVQV2RRg6MtgtMzc2Yxhnq1HVjA==", "159f6112-4a77-4073-919d-f30195cb4440" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("52aaa1d4-2d04-4b0f-8e95-2d73796d8333"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Welcome", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "WelcomeEmail", null, null },
                    { new Guid("a554d440-ac6a-4afa-868a-78ef5a2e2876"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Reset Your Password", "ForgotPasswordEmail", null, null },
                    { new Guid("ae566d7f-5930-4f76-928d-f1c97658bd87"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Cursus Verify Email", "SendVerifyEmail", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("52aaa1d4-2d04-4b0f-8e95-2d73796d8333"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("a554d440-ac6a-4afa-868a-78ef5a2e2876"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("ae566d7f-5930-4f76-928d-f1c97658bd87"));

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "DoctorRatings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0948f959-8f90-4fbf-9fbe-b8f6643e856b", "AQAAAAIAAYagAAAAEKgMRkcwAL5YX1Z072y8Eb8g18fC15HJpxN7swMDMkrbdAE1feBrXfngVnrBaFnWZg==", "fb5057d2-43c7-4495-9b9b-43e4dbdd602b" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("1a1815b9-5e42-4b32-ba0d-bad5b1e9b2d9"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Welcome", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "WelcomeEmail", null, null },
                    { new Guid("b0bc1703-2244-42dd-977d-0341cf7cad71"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("d971398c-cb8b-4d99-be81-cadeb0324d37"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Reset Your Password", "ForgotPasswordEmail", null, null }
                });
        }
    }
}
