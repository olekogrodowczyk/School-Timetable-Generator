using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class DeleteStudentsInClassCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classess_ClassId",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classess_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classess_ClassId",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classess_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
