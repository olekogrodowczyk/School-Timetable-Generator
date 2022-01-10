using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class makeClassIdInSubjectNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_TimetableId_Name_ClassId",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TimetableId_Name_ClassId",
                table: "Subjects",
                columns: new[] { "TimetableId", "Name", "ClassId" },
                unique: true,
                filter: "[TimetableId] IS NOT NULL AND [Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_TimetableId_Name_ClassId",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Subjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TimetableId_Name_ClassId",
                table: "Subjects",
                columns: new[] { "TimetableId", "Name", "ClassId" },
                unique: true,
                filter: "[TimetableId] IS NOT NULL AND [Name] IS NOT NULL AND [ClassId] IS NOT NULL");
        }
    }
}
