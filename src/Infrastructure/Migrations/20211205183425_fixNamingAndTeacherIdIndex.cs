using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class fixNamingAndTeacherIdIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_TimeTables_TimeTableId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Classes_ClassId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Classes",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes");

            migrationBuilder.RenameTable(
                name: "Classes",
                newName: "Classess");

            migrationBuilder.RenameColumn(
                name: "TimeTableId",
                table: "Classess",
                newName: "TimetableId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_TimeTableId",
                table: "Classess",
                newName: "IX_Classess_TimetableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classess",
                table: "Classess",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Classess_TeacherId",
                table: "Classess",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classess_Teachers_TeacherId",
                table: "Classess",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classess_TimeTables_TimetableId",
                table: "Classess",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Classess_ClassId",
                table: "Groups",
                column: "ClassId",
                principalTable: "Classess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classess_Teachers_TeacherId",
                table: "Classess");

            migrationBuilder.DropForeignKey(
                name: "FK_Classess_TimeTables_TimetableId",
                table: "Classess");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Classess_ClassId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Classess",
                table: "Classess");

            migrationBuilder.DropIndex(
                name: "IX_Classess_TeacherId",
                table: "Classess");

            migrationBuilder.RenameTable(
                name: "Classess",
                newName: "Classes");

            migrationBuilder.RenameColumn(
                name: "TimetableId",
                table: "Classes",
                newName: "TimeTableId");

            migrationBuilder.RenameIndex(
                name: "IX_Classess_TimetableId",
                table: "Classes",
                newName: "IX_Classes_TimeTableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classes",
                table: "Classes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_TimeTables_TimeTableId",
                table: "Classes",
                column: "TimeTableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Classes_ClassId",
                table: "Groups",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
