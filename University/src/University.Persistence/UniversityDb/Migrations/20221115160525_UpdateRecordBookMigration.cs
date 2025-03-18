using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Persistence.UniversityDb.Migrations
{
    public partial class UpdateRecordBookMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_RecordBooks_RecordBookId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_RecordBookId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RecordBookId",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "RecordBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RecordBooks_StudentId",
                table: "RecordBooks",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordBooks_Students_StudentId",
                table: "RecordBooks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordBooks_Students_StudentId",
                table: "RecordBooks");

            migrationBuilder.DropIndex(
                name: "IX_RecordBooks_StudentId",
                table: "RecordBooks");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "RecordBooks");

            migrationBuilder.AddColumn<Guid>(
                name: "RecordBookId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Students_RecordBookId",
                table: "Students",
                column: "RecordBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_RecordBooks_RecordBookId",
                table: "Students",
                column: "RecordBookId",
                principalTable: "RecordBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
