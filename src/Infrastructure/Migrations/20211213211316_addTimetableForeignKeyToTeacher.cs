using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addTimetableForeignKeyToTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_TimetableId",
                table: "Teachers",
                column: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_TimeTables_TimetableId",
                table: "Teachers",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_TimeTables_TimetableId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_TimetableId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "Teachers");
        }
    }
}
