using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiVeterinaryServiceCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Modify_AppointmentAppointmentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentStatus",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "BookingStatus",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "AppointmentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentStatuses_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f08afa9-5367-4c87-872f-ba79cc77f440", "AQAAAAIAAYagAAAAEAIG96QQUb9lPSQBI8DwqT0CB8xcCQRVFPRkrIcDWb/Qpzl8knXGRrgIiKKiibJEPQ==", "5b6b28bc-40b7-4129-a86e-20cc81c8b1b1" });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentStatuses_AppointmentId",
                table: "AppointmentStatuses",
                column: "AppointmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "BookingStatus",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AppointmentStatus",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BestZedAndYasuo",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4dfe8140-ee0d-4246-9ea2-81f76439146c", "AQAAAAIAAYagAAAAEDNrz1tAF/YLGF+pYAZZwgupNAYruScaykv9ZT2g1wJAy3gh31o4TUQOLAa3CL2GJg==", "074bdb71-b605-4704-9c1e-c1b0550d6e09" });
        }
    }
}
