using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class TeacherAvailabilityOnDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_Teachers_TeacherId",
                table: "Availabilities");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_Teachers_TeacherId",
                table: "Availabilities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_Teachers_TeacherId",
                table: "Availabilities");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_Teachers_TeacherId",
                table: "Availabilities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
