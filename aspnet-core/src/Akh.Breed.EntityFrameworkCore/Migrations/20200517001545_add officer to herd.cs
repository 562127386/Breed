using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class addofficertoherd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfficerId",
                table: "AkhHerds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerds_OfficerId",
                table: "AkhHerds",
                column: "OfficerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhHerds_AkhOfficers_OfficerId",
                table: "AkhHerds",
                column: "OfficerId",
                principalTable: "AkhOfficers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhHerds_AkhOfficers_OfficerId",
                table: "AkhHerds");

            migrationBuilder.DropIndex(
                name: "IX_AkhHerds_OfficerId",
                table: "AkhHerds");

            migrationBuilder.DropColumn(
                name: "OfficerId",
                table: "AkhHerds");
        }
    }
}
