using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class fixName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availability_Teachers_TeacherId",
                table: "Availability");

            migrationBuilder.DropForeignKey(
                name: "FK_Availability_TimeTables_TimetableId",
                table: "Availability");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Availability",
                table: "Availability");

            migrationBuilder.RenameTable(
                name: "Availability",
                newName: "Availabilities");

            migrationBuilder.RenameIndex(
                name: "IX_Availability_TimetableId",
                table: "Availabilities",
                newName: "IX_Availabilities_TimetableId");

            migrationBuilder.RenameIndex(
                name: "IX_Availability_TeacherId",
                table: "Availabilities",
                newName: "IX_Availabilities_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Availabilities",
                table: "Availabilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_Teachers_TeacherId",
                table: "Availabilities",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_TimeTables_TimetableId",
                table: "Availabilities",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_Teachers_TeacherId",
                table: "Availabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_TimeTables_TimetableId",
                table: "Availabilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Availabilities",
                table: "Availabilities");

            migrationBuilder.RenameTable(
                name: "Availabilities",
                newName: "Availability");

            migrationBuilder.RenameIndex(
                name: "IX_Availabilities_TimetableId",
                table: "Availability",
                newName: "IX_Availability_TimetableId");

            migrationBuilder.RenameIndex(
                name: "IX_Availabilities_TeacherId",
                table: "Availability",
                newName: "IX_Availability_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Availability",
                table: "Availability",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_Teachers_TeacherId",
                table: "Availability",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_TimeTables_TimetableId",
                table: "Availability",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
