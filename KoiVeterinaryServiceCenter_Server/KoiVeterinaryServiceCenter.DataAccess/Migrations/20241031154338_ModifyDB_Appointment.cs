using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDB_Appointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("0f269e06-03e9-45f9-b71c-52c54845e69d"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("b56a8215-083a-460a-9e7a-aee781d8bac7"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("c9db7772-0d05-43cd-8b8c-7d9a0d360e9e"));

            migrationBuilder.AddColumn<Guid>(
                name: "PetId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AdminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b8f2169-2abb-487e-952f-b4a6530835d6", "AQAAAAIAAYagAAAAEA7gMmHimx20PS0bF2+Gvb/P7YFhw0pDE1WuDGKQtbLxwApxKErSw3dfR9XGIwma1g==", "11f0bb88-6e7c-4947-ab1b-e69dbbb9c084" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54d5ef7b-d626-4e67-b14b-45ba56f304a1", "AQAAAAIAAYagAAAAENRgmvUuUT2gI79kKelWJOUQgZ4K6pbYeojn3w79TH1ZMyCxv65B5EOKiuMXMkaIYQ==", "1a195797-7cd1-4608-b769-a22613e2fb6b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97014143-20fd-42b1-8988-80355a4bfdec", "AQAAAAIAAYagAAAAEFETEn53LYEiUFtIhvAvd95rtCo9MubFsX0XrDum5WgrzBrS9AREFuCoGf3JQ9NNPg==", "d6258650-b607-45e6-8675-e5e1bf74120e" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("758bb7c7-be7e-4a07-bfbf-51af9cbb293b"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Reset Your Password", "ForgotPasswordEmail", null, null },
                    { new Guid("8f8a8d49-deca-42e1-8c09-7edfc5651a72"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("a62c0c60-fddd-490c-909b-3fd680820c91"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("758bb7c7-be7e-4a07-bfbf-51af9cbb293b"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("8f8a8d49-deca-42e1-8c09-7edfc5651a72"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("a62c0c60-fddd-490c-909b-3fd680820c91"));

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AdminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0176da9-d793-41e5-85bb-1acc77c31acc", "AQAAAAIAAYagAAAAEC5HsIulJ0RUJnFlVZBeK0tiSmo4k9JucvkRthmwHU4TyjCcKvhgEIoH0gHPzaWnyQ==", "fbf546a5-1bf1-45c1-aad5-13fc40d0b3b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf6ce091-6296-47f9-8ad6-b1bcbbfb06c6", "AQAAAAIAAYagAAAAENuBGYxMGs7wHWxXgeuxQhl2duf5FSlB2PGqTyRDkNoLe8nEyql41hC+F2WQjUUPVA==", "85f227fb-60b0-443d-8154-df26eca9257d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e192cb7b-919f-49c9-b0f7-82df088298c3", "AQAAAAIAAYagAAAAEG3gPhxvue7YoUUvSXs0vcffNXEyl6v6Kr4gtOkU3no5F/BVhqEF51OnVGPfmKLbNA==", "5209bdff-e7d0-49c2-be67-8ba4a861004b" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("0f269e06-03e9-45f9-b71c-52c54845e69d"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("b56a8215-083a-460a-9e7a-aee781d8bac7"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Reset Your Password", "ForgotPasswordEmail", null, null },
                    { new Guid("c9db7772-0d05-43cd-8b8c-7d9a0d360e9e"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null }
                });
        }
    }
}
