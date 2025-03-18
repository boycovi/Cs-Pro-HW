using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Persistence.UniversityDb.Migrations
{
    public partial class AddRecordBookMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecordBookId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RecordBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordBooksSubjects",
                columns: table => new
                {
                    RecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordBooksSubjects", x => new { x.RecordId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_RecordBooksSubjects_RecordBooks_RecordId",
                        column: x => x.RecordId,
                        principalTable: "RecordBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecordBooksSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_RecordBookId",
                table: "Students",
                column: "RecordBookId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordBooksSubjects_SubjectId",
                table: "RecordBooksSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_RecordBooks_RecordBookId",
                table: "Students",
                column: "RecordBookId",
                principalTable: "RecordBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_RecordBooks_RecordBookId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "RecordBooksSubjects");

            migrationBuilder.DropTable(
                name: "RecordBooks");

            migrationBuilder.DropIndex(
                name: "IX_Students_RecordBookId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RecordBookId",
                table: "Students");
        }
    }
}
