using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class unionuserchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AkhUnionInfo",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AkhContractors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhUnionInfo_UserId",
                table: "AkhUnionInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhContractors_UserId",
                table: "AkhContractors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AbpUsers_UserId",
                table: "AkhContractors",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhUnionInfo_AbpUsers_UserId",
                table: "AkhUnionInfo",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AbpUsers_UserId",
                table: "AkhContractors");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhUnionInfo_AbpUsers_UserId",
                table: "AkhUnionInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhUnionInfo_UserId",
                table: "AkhUnionInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhContractors_UserId",
                table: "AkhContractors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AkhUnionInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AkhContractors");
        }
    }
}
