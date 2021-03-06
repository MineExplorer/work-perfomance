using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ClientAssesment = table.Column<float>(type: "real", nullable: false),
                    PerfomanceAssesment = table.Column<float>(type: "real", nullable: false),
                    CodeQualityAssesment = table.Column<float>(type: "real", nullable: false),
                    TeamworkAssesment = table.Column<float>(type: "real", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeFeedbacks_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFeedbacks_EmployeeId",
                table: "EmployeeFeedbacks",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeFeedbacks");
        }
    }
}
