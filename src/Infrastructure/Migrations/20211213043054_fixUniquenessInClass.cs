using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class fixUniquenessInClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Classess_Name",
                table: "Classess");

            migrationBuilder.DropIndex(
                name: "IX_Classess_TimetableId",
                table: "Classess");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Classess_TimetableId_Name",
                table: "Classess",
                columns: new[] { "TimetableId", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Classess_TimetableId_Name",
                table: "Classess");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Classess_Name",
                table: "Classess",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Classess_TimetableId",
                table: "Classess",
                column: "TimetableId");
        }
    }
}
