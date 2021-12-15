using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addUniquenessIntoGroupName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_TimeTables_TimetableId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TimetableId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Groups_TimetableId_Name",
                table: "Groups",
                columns: new[] { "TimetableId", "Name" });

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_TimeTables_TimetableId",
                table: "Groups",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_TimeTables_TimetableId",
                table: "Groups");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Groups_TimetableId_Name",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TimetableId",
                table: "Groups",
                column: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_TimeTables_TimetableId",
                table: "Groups",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
