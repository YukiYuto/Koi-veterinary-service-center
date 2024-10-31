using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDB_Seedings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8fa7c7bb-daa5-a660-bf02-82301a5eb327", "BestZedAndYasuo" });

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("c707e3cd-bd21-43d7-ad9d-dfcadc879b40"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("d5e0a164-ba27-4b6a-9701-4ba308704cc7"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("eaefc834-4f89-459a-90a9-880ddf1054a1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8sa7c7bb-b4dc-480d-a660-82301a5eb327", "STAFF", "STAFF", "STAFF" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AvatarUrl", "BirthDate", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "AdminId", 0, "123 Admin St", "https://example.com/avatar.png", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1e0d5480-6c3a-47fb-b261-dc817e0b8649", "Country", "admin@gmail.com", true, "Admin User", "Male", true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEBgcwPHzBtdLIKhKE4HfxVQvN9q3k4Mw1/mZV2cFNqYh2kIo/LFs5poIHocg5c650Q==", "1234567890", true, "fcd6b265-db5f-458a-8561-ac17ccc1d71a", false, "admin@gmail.com" },
                    { "StaffId", 0, "123 Staff St", "https://example.com/avatarStaff.png", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4b5e7638-3afd-4f63-b79b-2f1372a09f47", "Country", "staff1@gmail.com", true, "Staff_1 User", "Male", true, null, "STAFF1@GMAIL.COM", "STAFF1@GMAIL.COM", "AQAAAAIAAYagAAAAEHeRPw+GkRdC9lj/6ql46Y5kIKhmu/en8SjLkon9sYOwTJHZyo7NNDpNRK9TG5gWCA==", "0123456789", true, "6827ae27-3a53-426c-ba62-10a1aefffc76", false, "staff1@gmail.com" },
                    { "StaffId2", 0, "456 Staff St", "https://example.com/avatarStaff2.png", new DateTime(1991, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "f23a46c2-9f98-40a6-83cd-1c1a22003ec0", "Country", "staff2@gmail.com", true, "Staff_2 User", "Female", true, null, "STAFF2@GMAIL.COM", "STAFF2@GMAIL.COM", "AQAAAAIAAYagAAAAEEwS8OLoNi+kBIoZd/iCehS2EOaS6IGvTrLMS89C59Z3Xa+9NjFnA+A+b+OC9QMXiA==", "0987654321", true, "9dbaa0d9-843b-47d1-baaf-06a24d7efd00", false, "staff2@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("5390f6ea-cdab-4f8b-8c97-3782cc1e0139"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null },
                    { new Guid("ed62d076-0a2a-47c7-a301-7077b7c514bb"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("fbde3d2a-d634-4a3e-8616-f0f7e72cd2db"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Reset Your Password", "ForgotPasswordEmail", null, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8fa7c7bb-daa5-a660-bf02-82301a5eb327", "AdminId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8sa7c7bb-b4dc-480d-a660-82301a5eb327");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8fa7c7bb-daa5-a660-bf02-82301a5eb327", "AdminId" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId2");

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

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AdminId");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AvatarUrl", "BirthDate", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "BestZedAndYasuo", 0, "123 Admin St", "https://example.com/avatar.png", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8db5091a-96ac-438c-927b-258dd65b08ce", "Country", "admin@gmail.com", true, "Admin User", "Male", true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAELKamXATd2BJhTGoX9eP9U/a3SF4mZzgvI440CepvF3fLssi3UFce+/nyOIHrDhxrQ==", "1234567890", true, "af4a3907-18e2-4d59-a586-f8c06b11329b", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("c707e3cd-bd21-43d7-ad9d-dfcadc879b40"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("d5e0a164-ba27-4b6a-9701-4ba308704cc7"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null },
                    { new Guid("eaefc834-4f89-459a-90a9-880ddf1054a1"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "cursusservicetts@gmail.com", "Cursus Team", 1, "Reset Your Password", "ForgotPasswordEmail", null, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8fa7c7bb-daa5-a660-bf02-82301a5eb327", "BestZedAndYasuo" });
        }
    }
}
