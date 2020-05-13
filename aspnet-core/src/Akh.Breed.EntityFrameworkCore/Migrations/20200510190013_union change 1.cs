using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class unionchange1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AkhUnionInfo_Code",
                table: "AkhUnionInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhUnionInfo_Name",
                table: "AkhUnionInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhUnionInfo_StateInfoId",
                table: "AkhUnionInfo");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AkhUnionInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhUnionInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UnionName",
                table: "AkhUnionInfo",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AkhUnionInfo_StateInfoId",
                table: "AkhUnionInfo",
                column: "StateInfoId",
                unique: true,
                filter: "[StateInfoId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AkhUnionInfo_StateInfoId",
                table: "AkhUnionInfo");

            migrationBuilder.DropColumn(
                name: "UnionName",
                table: "AkhUnionInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhUnionInfo",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AkhUnionInfo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AkhUnionInfo_Code",
                table: "AkhUnionInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhUnionInfo_Name",
                table: "AkhUnionInfo",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhUnionInfo_StateInfoId",
                table: "AkhUnionInfo",
                column: "StateInfoId");
        }
    }
}
