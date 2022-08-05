using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Add_Project_Archive_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ProjectEmployees");

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Projects");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ProjectEmployees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
