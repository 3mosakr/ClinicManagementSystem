using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyBetweenDoctorAndPatent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserPatient",
                columns: table => new
                {
                    PatientsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPatient", x => new { x.PatientsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserPatient_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserPatient_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPatient_UsersId",
                table: "ApplicationUserPatient",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserPatient");
        }
    }
}
