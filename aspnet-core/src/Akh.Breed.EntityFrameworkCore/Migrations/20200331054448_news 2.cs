using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class news2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabaled",
                table: "AkhNotices");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "AkhNotices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_AkhNotices_UserId",
                table: "AkhNotices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhNotices_AbpUsers_UserId",
                table: "AkhNotices",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhNotices_AbpUsers_UserId",
                table: "AkhNotices");

            migrationBuilder.DropIndex(
                name: "IX_AkhNotices_UserId",
                table: "AkhNotices");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "AkhNotices");

            migrationBuilder.AddColumn<bool>(
                name: "Enabaled",
                table: "AkhNotices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
