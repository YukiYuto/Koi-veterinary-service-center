using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDB_Pet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("52e01d8b-1700-45e3-8dc6-2a0d3da219ed"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("9483b8fa-32f2-4a1e-a7e4-ff8b23b910f6"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("cc6ed3b4-8d6e-4ed0-a365-e311d6c59166"));

            migrationBuilder.RenameColumn(
                name: "Breed",
                table: "Pets",
                newName: "Description");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AdminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cdf860d1-d9b7-4012-a517-6b80084034c5", "AQAAAAIAAYagAAAAEJD4kCJ/P8SuTOEJYKt7HOYrMfHgDUmV/cF+Yzolxh92OzTi8RA0rH9maxwtJ9c4rQ==", "6fda5a7d-20da-462c-92d3-22087ff3606b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a72191a7-cc4d-4c6d-b10c-3a8711aea8bd", "AQAAAAIAAYagAAAAEM6vc/jFumnUOHFXPdfawmJf/u65ClZZJzYQ0LXip6FS1oPbEYzkec2jGfRtPA3Y6w==", "6d506f76-971b-4643-af5e-d1b7b22c8b67" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2bd48bd-34cc-499d-bf99-f755b567fb5c", "AQAAAAIAAYagAAAAEJ38VnAcr0x7yH2pLCw73LyMR4BMIkwqCRgL1o9ScB79iUkYFHuiSqFc8WYCFhm27g==", "e10da5e3-46dd-41d2-9891-ce6debedb19c" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("14023b5b-5bbe-4352-849a-86d284818380"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("1df8bc6b-a20b-4f7c-b398-f5a6bea708b5"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Reset Your Password", "ForgotPasswordEmail", null, null },
                    { new Guid("297db0a8-797e-4019-b685-4f40f19dcabc"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("14023b5b-5bbe-4352-849a-86d284818380"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("1df8bc6b-a20b-4f7c-b398-f5a6bea708b5"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("297db0a8-797e-4019-b685-4f40f19dcabc"));

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Pets",
                newName: "Breed");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AdminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bca1617a-2fbb-4125-81b1-e709468bbadd", "AQAAAAIAAYagAAAAEI0gjogOyvpg0/GM2f/DUN9LPbWro/IRLjVNES3rTlh0INEKcqzn6nnYnqFqe9t8Hw==", "0f71c43e-0be4-46d7-bd11-ad1d75855b4c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d28db11e-209a-4c0d-80a0-8aa5ef6a185a", "AQAAAAIAAYagAAAAEHQ8kqe8VJhnnR8nHBgQxCqcT/9/rm8jkX2YOeiTFkjZNnUx2sUkDMdRK8NOZnxQOQ==", "078fe096-3b48-4e33-b276-4336e3c4f802" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff2dba98-3d19-4995-8f80-8f2a01d8b351", "AQAAAAIAAYagAAAAECg61nIUzJUohvYlJXdV4eNv0lLuoJYu53nDcSTjh/dhUXZX0QFIzo9B9XGjw0LbAA==", "6d114b07-cda3-4173-b769-8f7035fa49e1" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("52e01d8b-1700-45e3-8dc6-2a0d3da219ed"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Reset Your Password", "ForgotPasswordEmail", null, null },
                    { new Guid("9483b8fa-32f2-4a1e-a7e4-ff8b23b910f6"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("cc6ed3b4-8d6e-4ed0-a365-e311d6c59166"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null }
                });
        }
    }
}
