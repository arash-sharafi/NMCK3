using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NMCK3.Infrastructure.Persistence.Migrations
{
    public partial class FixExReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamReservation_Exams_ExamId1",
                table: "ExamReservation");

            migrationBuilder.DropIndex(
                name: "IX_ExamReservation_ExamId1",
                table: "ExamReservation");

            migrationBuilder.DropColumn(
                name: "ExamId1",
                table: "ExamReservation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExamId1",
                table: "ExamReservation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamReservation_ExamId1",
                table: "ExamReservation",
                column: "ExamId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamReservation_Exams_ExamId1",
                table: "ExamReservation",
                column: "ExamId1",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
