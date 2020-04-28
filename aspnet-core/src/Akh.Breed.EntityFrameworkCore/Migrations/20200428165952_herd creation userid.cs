using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class herdcreationuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "AkhLivestocks",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "AkhHerds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "AkhLivestocks");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "AkhHerds");
        }
    }
}
