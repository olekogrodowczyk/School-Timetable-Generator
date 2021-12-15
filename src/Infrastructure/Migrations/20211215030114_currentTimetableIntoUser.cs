using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class currentTimetableIntoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTimetableId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentTimetableId",
                table: "Users",
                column: "CurrentTimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TimeTables_CurrentTimetableId",
                table: "Users",
                column: "CurrentTimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TimeTables_CurrentTimetableId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CurrentTimetableId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentTimetableId",
                table: "Users");
        }
    }
}
