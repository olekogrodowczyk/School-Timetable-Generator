using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addTimetableForeignKeyIntoAllRelateEntities : Migration
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
                name: "FK_Subjects_TimeTables_TimetableId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_TimeTables_TimetableId",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "Availability",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "Assignments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_TimetableId",
                table: "Students",
                column: "TimetableId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TimetableId",
                table: "Lessons",
                column: "TimetableId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TimetableId",
                table: "Groups",
                column: "TimetableId");

            migrationBuilder.CreateIndex(
                name: "IX_Availability_TimetableId",
                table: "Availability",
                column: "TimetableId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_TimetableId",
                table: "Assignments",
                column: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_TimeTables_TimetableId",
                table: "Assignments",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_TimeTables_TimetableId",
                table: "Availability",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_TimeTables_TimetableId",
                table: "Lessons",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_TimeTables_TimetableId",
                table: "Students",
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_TimeTables_TimetableId",
                table: "Teachers",
                column: "TimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_TimeTables_TimetableId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Availability_TimeTables_TimetableId",
                table: "Availability");

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
                name: "FK_Lessons_TimeTables_TimetableId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_TimeTables_TimetableId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_TimeTables_TimetableId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_TimeTables_TimetableId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_TimetableId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TimetableId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TimetableId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Availability_TimetableId",
                table: "Availability");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_TimetableId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "Assignments");

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
    }
}
