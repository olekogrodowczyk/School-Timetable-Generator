using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addClassIdColumnIntoSubjectWithUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_TimetableId_Name",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ClassId",
                table: "Subjects",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TimetableId_Name_ClassId",
                table: "Subjects",
                columns: new[] { "TimetableId", "Name", "ClassId" },
                unique: true,
                filter: "[TimetableId] IS NOT NULL AND [Name] IS NOT NULL AND [ClassId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Classess_ClassId",
                table: "Subjects",
                column: "ClassId",
                principalTable: "Classess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Classess_ClassId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ClassId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TimetableId_Name_ClassId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Subjects");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TimetableId_Name",
                table: "Subjects",
                columns: new[] { "TimetableId", "Name" },
                unique: true,
                filter: "[TimetableId] IS NOT NULL AND [Name] IS NOT NULL");
        }
    }
}
