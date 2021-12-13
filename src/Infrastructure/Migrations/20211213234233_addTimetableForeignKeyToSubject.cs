using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addTimetableForeignKeyToSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TimetableId",
                table: "Subjects",
                column: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_TimeTables_TimetableId",
                table: "Subjects",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_TimeTables_TimetableId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TimetableId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "Subjects");
        }
    }
}
