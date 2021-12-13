using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addTimetableForeignKeyToClassroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "Classrooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_TimetableId",
                table: "Classrooms",
                column: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_TimeTables_TimetableId",
                table: "Classrooms",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_TimeTables_TimetableId",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_TimetableId",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "Classrooms");
        }
    }
}
