using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class MakeIndexesUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teachers_TimetableId_FirstName_LastName",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TimetableId_Name",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TimetableId_Name",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_TimetableId_Code",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Classess_TimetableId_Name",
                table: "Classess");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_TimetableId_FirstName_LastName",
                table: "Teachers",
                columns: new[] { "TimetableId", "FirstName", "LastName" },
                unique: true,
                filter: "[TimetableId] IS NOT NULL AND [FirstName] IS NOT NULL AND [LastName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TimetableId_Name",
                table: "Subjects",
                columns: new[] { "TimetableId", "Name" },
                unique: true,
                filter: "[TimetableId] IS NOT NULL AND [Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TimetableId_Name",
                table: "Groups",
                columns: new[] { "TimetableId", "Name" },
                unique: true,
                filter: "[TimetableId] IS NOT NULL AND [Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_TimetableId_Code",
                table: "Classrooms",
                columns: new[] { "TimetableId", "Code" },
                unique: true,
                filter: "[TimetableId] IS NOT NULL AND [Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classess_TimetableId_Name",
                table: "Classess",
                columns: new[] { "TimetableId", "Name" },
                unique: true,
                filter: "[TimetableId] IS NOT NULL AND [Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teachers_TimetableId_FirstName_LastName",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TimetableId_Name",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TimetableId_Name",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_TimetableId_Code",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Classess_TimetableId_Name",
                table: "Classess");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_TimetableId_FirstName_LastName",
                table: "Teachers",
                columns: new[] { "TimetableId", "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TimetableId_Name",
                table: "Subjects",
                columns: new[] { "TimetableId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TimetableId_Name",
                table: "Groups",
                columns: new[] { "TimetableId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_TimetableId_Code",
                table: "Classrooms",
                columns: new[] { "TimetableId", "Code" });

            migrationBuilder.CreateIndex(
                name: "IX_Classess_TimetableId_Name",
                table: "Classess",
                columns: new[] { "TimetableId", "Name" });
        }
    }
}
