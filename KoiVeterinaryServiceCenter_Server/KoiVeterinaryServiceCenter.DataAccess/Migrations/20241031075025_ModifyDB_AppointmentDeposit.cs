using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDB_AppointmentDeposit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("5390f6ea-cdab-4f8b-8c97-3782cc1e0139"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("ed62d076-0a2a-47c7-a301-7077b7c514bb"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("fbde3d2a-d634-4a3e-8616-f0f7e72cd2db"));

            migrationBuilder.AddColumn<long>(
                name: "AppointmentDepositNumbe",
                table: "Appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AppointmentDepositNumber",
                table: "AppointmentDeposits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AppointmentDepositNumbe",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentDepositNumber",
                table: "AppointmentDeposits");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AdminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e0d5480-6c3a-47fb-b261-dc817e0b8649", "AQAAAAIAAYagAAAAEBgcwPHzBtdLIKhKE4HfxVQvN9q3k4Mw1/mZV2cFNqYh2kIo/LFs5poIHocg5c650Q==", "fcd6b265-db5f-458a-8561-ac17ccc1d71a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b5e7638-3afd-4f63-b79b-2f1372a09f47", "AQAAAAIAAYagAAAAEHeRPw+GkRdC9lj/6ql46Y5kIKhmu/en8SjLkon9sYOwTJHZyo7NNDpNRK9TG5gWCA==", "6827ae27-3a53-426c-ba62-10a1aefffc76" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f23a46c2-9f98-40a6-83cd-1c1a22003ec0", "AQAAAAIAAYagAAAAEEwS8OLoNi+kBIoZd/iCehS2EOaS6IGvTrLMS89C59Z3Xa+9NjFnA+A+b+OC9QMXiA==", "9dbaa0d9-843b-47d1-baaf-06a24d7efd00" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("5390f6ea-cdab-4f8b-8c97-3782cc1e0139"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null },
                    { new Guid("ed62d076-0a2a-47c7-a301-7077b7c514bb"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("fbde3d2a-d634-4a3e-8616-f0f7e72cd2db"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Reset Your Password", "ForgotPasswordEmail", null, null }
                });
        }
    }
}
