using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorAvailabilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_DoctorAvailability_DoctorAvailabilityId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAvailability_AspNetUsers_DoctorId",
                table: "DoctorAvailability");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorAvailability",
                table: "DoctorAvailability");

            migrationBuilder.RenameTable(
                name: "DoctorAvailability",
                newName: "DoctorAvailabilities");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorAvailability_DoctorId",
                table: "DoctorAvailabilities",
                newName: "IX_DoctorAvailabilities_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorAvailabilities",
                table: "DoctorAvailabilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_DoctorAvailabilities_DoctorAvailabilityId",
                table: "Appointments",
                column: "DoctorAvailabilityId",
                principalTable: "DoctorAvailabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAvailabilities_AspNetUsers_DoctorId",
                table: "DoctorAvailabilities",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_DoctorAvailabilities_DoctorAvailabilityId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAvailabilities_AspNetUsers_DoctorId",
                table: "DoctorAvailabilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorAvailabilities",
                table: "DoctorAvailabilities");

            migrationBuilder.RenameTable(
                name: "DoctorAvailabilities",
                newName: "DoctorAvailability");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorAvailabilities_DoctorId",
                table: "DoctorAvailability",
                newName: "IX_DoctorAvailability_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorAvailability",
                table: "DoctorAvailability",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_DoctorAvailability_DoctorAvailabilityId",
                table: "Appointments",
                column: "DoctorAvailabilityId",
                principalTable: "DoctorAvailability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAvailability_AspNetUsers_DoctorId",
                table: "DoctorAvailability",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
