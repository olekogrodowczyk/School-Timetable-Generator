using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addClassroomForeignKeyIntoGroupEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ClassroomId",
                table: "Groups",
                column: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Classrooms_ClassroomId",
                table: "Groups",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Classrooms_ClassroomId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ClassroomId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "Groups");
        }
    }
}
