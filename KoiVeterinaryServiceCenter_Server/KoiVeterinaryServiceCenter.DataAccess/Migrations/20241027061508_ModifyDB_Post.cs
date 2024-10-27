using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDB_Post : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("58ac227e-6cf8-40af-8c0a-5aa3f0394ddf"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("dc7e3875-d81d-4d4b-881c-d745c6f5dd0b"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("f6355d91-6797-4c86-8339-aec3ba66b0c2"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5065041c-6e04-425d-8735-40afc5bc496b", "AQAAAAIAAYagAAAAECXdJiPBTb6bWXO8uAzTgcaAK7bOsFMgtIUeNWQ2extr0mylMcfAbrWyWkGqGkdhdQ==", "4f9df4b7-1cf4-4269-88c2-f324dab2e08d" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("8586acf6-8437-4648-b4ae-83a7b433cf8d"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Reset Your Password", "ForgotPasswordEmail", null, null },
                    { new Guid("976727cf-2811-44da-a24b-248ae131ba98"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Welcome", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "WelcomeEmail", null, null },
                    { new Guid("9d66c406-c412-4fc4-bc86-c06f8c4404a9"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Cursus Verify Email", "SendVerifyEmail", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("8586acf6-8437-4648-b4ae-83a7b433cf8d"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("976727cf-2811-44da-a24b-248ae131ba98"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("9d66c406-c412-4fc4-bc86-c06f8c4404a9"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d79c31d-b51a-4c38-add0-d731f34265bf", "AQAAAAIAAYagAAAAEMyP0QJhO7ARqYNNbtLvwzAKNUUP8155Rne4gfuFggecSdnAB6RKxlPLhDNjOVjQcQ==", "9a900c68-9528-47e2-9b5b-81a2050ccf55" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("58ac227e-6cf8-40af-8c0a-5aa3f0394ddf"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Welcome", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "WelcomeEmail", null, null },
                    { new Guid("dc7e3875-d81d-4d4b-881c-d745c6f5dd0b"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Reset Your Password", "ForgotPasswordEmail", null, null },
                    { new Guid("f6355d91-6797-4c86-8339-aec3ba66b0c2"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Cursus Verify Email", "SendVerifyEmail", null, null }
                });
        }
    }
}
