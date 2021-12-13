using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class fixUniquenessInClassroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Classrooms_Code",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_TimetableId",
                table: "Classrooms");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Classrooms_TimetableId_Code",
                table: "Classrooms",
                columns: new[] { "TimetableId", "Code" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Classrooms_TimetableId_Code",
                table: "Classrooms");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Classrooms_Code",
                table: "Classrooms",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_TimetableId",
                table: "Classrooms",
                column: "TimetableId");
        }
    }
}
