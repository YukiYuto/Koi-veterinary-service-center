using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DoctorRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("9f72a3a1-782a-41a0-98a1-2d5ddafe2abf"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("b65e1360-4660-4544-8e8b-e9cec50c7714"));

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: new Guid("d768da24-e5aa-4c39-a08e-17479f0a92b7"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "DoctorRatings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_DoctorRatings_AppointmentId",
                table: "DoctorRatings",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorRatings_Appointments_AppointmentId",
                table: "DoctorRatings",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorRatings_Appointments_AppointmentId",
                table: "DoctorRatings");

            migrationBuilder.DropIndex(
                name: "IX_DoctorRatings_AppointmentId",
                table: "DoctorRatings");

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

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "DoctorRatings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AdminId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1202ed9e-4794-4d07-be16-a4971147492d", "AQAAAAIAAYagAAAAEMyrvyyCaUS4uAXFZsGFmDyqq7E62ZqDdQC+iGZfMljncpH0hNTj0MPQ4xkK3euY9g==", "6b413b87-8d7f-4178-9b34-40a294ea2d05" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45a4e149-b4c1-449e-9e9d-9c30156bc76f", "AQAAAAIAAYagAAAAEE9A6+l1TqdALFYhbj87Ziy/t8uEILOvjFOhfZ0M9T1p2a/aUuEZKMycKUdUUEVoTQ==", "fe508184-a509-48b4-9d50-f09ca5618ff0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "StaffId2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9a41380-3329-4221-b411-8da10e348e81", "AQAAAAIAAYagAAAAEPpeLrqv9KvTHlpazPtCbtxhYC9GhzQuWoSkftscJlnu8TC5xYoNnIVO6k0TEU8Kgw==", "ddd7f5da-fca7-44c0-9d6c-7d6e54aa38e5" });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "BodyContent", "CallToAction", "Category", "CreatedBy", "CreatedTime", "FooterContent", "Language", "PersonalizationTags", "PreHeaderText", "RecipientType", "SenderEmail", "SenderName", "Status", "SubjectLine", "TemplateName", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("9f72a3a1-782a-41a0-98a1-2d5ddafe2abf"), "<p>Thank you for registering your Cursus account. Click here to go back the page</p>", "<a href=\"{{Login}}\">Login now</a>", "Verify", null, null, "<p>Contact us at cursusservicetts@gmail.com</p>", "English", "{FirstName}, {LinkLogin}", "User Account Verified!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Cursus Verify Email", "SendVerifyEmail", null, null },
                    { new Guid("b65e1360-4660-4544-8e8b-e9cec50c7714"), "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.", "https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)", "Security", null, null, "If you did not request a password reset, please ignore this email.", "English", "[UserFullName], [ResetPasswordLink]", "Reset your password to regain access", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Reset Your Password", "ForgotPasswordEmail", null, null },
                    { new Guid("d768da24-e5aa-4c39-a08e-17479f0a92b7"), "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.", "<a href=\"{{VerificationLink}}\">Verify Your Email</a>", "Appointment", null, null, "<p>Contact us at koiveterinaryservice@gmail.com</p>", "English", "{FirstName}, {LastName}", "Thank you for signing up!", "Customer", "koiveterinaryservice@gmail.com", "Koi Veterinary Service Center", 1, "Welcome to Koi Veterinary Service Center!", "Appointment booked", null, null }
                });
        }
    }
}
