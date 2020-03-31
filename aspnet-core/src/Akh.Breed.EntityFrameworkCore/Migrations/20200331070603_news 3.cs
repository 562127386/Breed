using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class news3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "AkhNotices");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "AkhNotices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "AkhNotices");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "AkhNotices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
