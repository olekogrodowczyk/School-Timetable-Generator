using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeGroupForeignKeyIntoClassInUnassignedLessons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnassignedLessons_Groups_GroupId",
                table: "UnassignedLessons");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "UnassignedLessons",
                newName: "ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_UnassignedLessons_GroupId",
                table: "UnassignedLessons",
                newName: "IX_UnassignedLessons_ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnassignedLessons_Classess_ClassId",
                table: "UnassignedLessons",
                column: "ClassId",
                principalTable: "Classess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnassignedLessons_Classess_ClassId",
                table: "UnassignedLessons");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "UnassignedLessons",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_UnassignedLessons_ClassId",
                table: "UnassignedLessons",
                newName: "IX_UnassignedLessons_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnassignedLessons_Groups_GroupId",
                table: "UnassignedLessons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
