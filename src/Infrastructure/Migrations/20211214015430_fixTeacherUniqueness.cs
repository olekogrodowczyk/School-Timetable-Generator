using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class fixTeacherUniqueness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Teachers_FirstName_LastName",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_TimetableId",
                table: "Teachers");

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Teachers_TimetableId_FirstName_LastName",
                table: "Teachers",
                columns: new[] { "TimetableId", "FirstName", "LastName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Teachers_TimetableId_FirstName_LastName",
                table: "Teachers");

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Teachers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Teachers_FirstName_LastName",
                table: "Teachers",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_TimetableId",
                table: "Teachers",
                column: "TimetableId");
        }
    }
}
