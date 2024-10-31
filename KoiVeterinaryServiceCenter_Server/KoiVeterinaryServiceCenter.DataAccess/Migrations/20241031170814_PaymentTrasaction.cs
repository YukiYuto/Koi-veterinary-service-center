using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTrasaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentNumber",
                table: "PaymentTransactions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "AppointmentDepositNumber",
                table: "PaymentTransactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AdminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfe5e887-93d5-4989-bbd6-ccd8e17dd66b", "AQAAAAIAAYagAAAAECefAv79NH8OoBFT1dFt3HV20sgnOCU64ZI08haeJcGTeDa/NWcXbafZNGEYsaQ7Dw==", "61c74d24-60f3-44bb-a925-fef77f1ddc23" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d42c43cb-f5e4-4000-90a3-2f94b1310ce0", "AQAAAAIAAYagAAAAEG2E2Y+OiDlCVTQ3z9g/JFQcVEeBbDK5is6MV8k3m25ifVKJGpnyhXl/vr57a9Qg7A==", "c1a4012d-11f0-419d-a5fc-7d471852341d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8e5b957-144b-42d6-be92-c22d77bf10dc", "AQAAAAIAAYagAAAAEMVohsxDBXVThXNHhCMKFWpLv8eGC9ypTOf9a/+OHtc6bKmxkhxmp7AEAXhDoiWZYA==", "d10aefb1-dbad-4101-90d3-a5e8f8986534" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("43bfdcca-0133-45c6-96f8-55f0cd8c18ad"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Reset Your Password", "ForgotPasswordEmail", null, null },
                    { new Guid("a1474613-036c-4427-98b9-7138c2103725"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null },
                    { new Guid("d06d9d9f-f4ba-4a80-96ab-339af0f26761"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Cursus Verify Email", "SendVerifyEmail", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("43bfdcca-0133-45c6-96f8-55f0cd8c18ad"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("a1474613-036c-4427-98b9-7138c2103725"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("d06d9d9f-f4ba-4a80-96ab-339af0f26761"));

            migrationBuilder.DropColumn(
                name: "AppointmentDepositNumber",
                table: "PaymentTransactions");

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentNumber",
                table: "PaymentTransactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
    }
}
