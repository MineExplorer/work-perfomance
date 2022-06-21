using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddExternalIDAndHourlyRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechStack",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "ExternalID",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExternalID",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "HourlyRate",
                table: "Employees",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "WorkTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Совещание");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ExternalID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "TechStack",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "WorkTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Встреча");
        }
    }
}
