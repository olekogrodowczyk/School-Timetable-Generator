using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changeAlternateKeysIntoIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classess_TimeTables_TimetableId",
                table: "Classess");

            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_TimeTables_TimetableId",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_TimeTables_TimetableId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_TimeTables_TimetableId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_TimeTables_TimetableId",
                table: "Teachers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Teachers_TimetableId_FirstName_LastName",
                table: "Teachers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Subjects_TimetableId_Name",
                table: "Subjects");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Groups_TimetableId_Name",
                table: "Groups");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Classrooms_TimetableId_Code",
                table: "Classrooms");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Classess_TimetableId_Name",
                table: "Classess");

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Teachers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Subjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Classrooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Classrooms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Classess",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Classess",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Classess_TimeTables_TimetableId",
                table: "Classess",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_TimeTables_TimetableId",
                table: "Classrooms",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_TimeTables_TimetableId",
                table: "Groups",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_TimeTables_TimetableId",
                table: "Subjects",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_TimeTables_TimetableId",
                table: "Teachers",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classess_TimeTables_TimetableId",
                table: "Classess");

            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_TimeTables_TimetableId",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_TimeTables_TimetableId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_TimeTables_TimetableId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_TimeTables_TimetableId",
                table: "Teachers");

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

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Classrooms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimetableId",
                table: "Classess",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Classess",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Teachers_TimetableId_FirstName_LastName",
                table: "Teachers",
                columns: new[] { "TimetableId", "FirstName", "LastName" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Subjects_TimetableId_Name",
                table: "Subjects",
                columns: new[] { "TimetableId", "Name" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Groups_TimetableId_Name",
                table: "Groups",
                columns: new[] { "TimetableId", "Name" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Classrooms_TimetableId_Code",
                table: "Classrooms",
                columns: new[] { "TimetableId", "Code" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Classess_TimetableId_Name",
                table: "Classess",
                columns: new[] { "TimetableId", "Name" });

            migrationBuilder.AddForeignKey(
                name: "FK_Classess_TimeTables_TimetableId",
                table: "Classess",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_TimeTables_TimetableId",
                table: "Classrooms",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_TimeTables_TimetableId",
                table: "Groups",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_TimeTables_TimetableId",
                table: "Subjects",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_TimeTables_TimetableId",
                table: "Teachers",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
