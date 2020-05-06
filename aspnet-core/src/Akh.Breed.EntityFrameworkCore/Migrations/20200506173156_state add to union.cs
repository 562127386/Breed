using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class stateaddtounion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhCityInfo_AkhStateInfo_StateInfoId",
                table: "AkhCityInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhCityInfo_Code_StateInfoId",
                table: "AkhCityInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhCityInfo_Name_StateInfoId",
                table: "AkhCityInfo");

            migrationBuilder.AddColumn<int>(
                name: "StateInfoId",
                table: "AkhUnionInfo",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateInfoId",
                table: "AkhCityInfo",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_AkhUnionInfo_StateInfoId",
                table: "AkhUnionInfo",
                column: "StateInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhCityInfo_Code_StateInfoId",
                table: "AkhCityInfo",
                columns: new[] { "Code", "StateInfoId" },
                unique: true,
                filter: "[StateInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AkhCityInfo_Name_StateInfoId",
                table: "AkhCityInfo",
                columns: new[] { "Name", "StateInfoId" },
                unique: true,
                filter: "[StateInfoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhCityInfo_AkhStateInfo_StateInfoId",
                table: "AkhCityInfo",
                column: "StateInfoId",
                principalTable: "AkhStateInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhUnionInfo_AkhStateInfo_StateInfoId",
                table: "AkhUnionInfo",
                column: "StateInfoId",
                principalTable: "AkhStateInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhCityInfo_AkhStateInfo_StateInfoId",
                table: "AkhCityInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_AkhUnionInfo_AkhStateInfo_StateInfoId",
                table: "AkhUnionInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhUnionInfo_StateInfoId",
                table: "AkhUnionInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhCityInfo_Code_StateInfoId",
                table: "AkhCityInfo");

            migrationBuilder.DropIndex(
                name: "IX_AkhCityInfo_Name_StateInfoId",
                table: "AkhCityInfo");

            migrationBuilder.DropColumn(
                name: "StateInfoId",
                table: "AkhUnionInfo");

            migrationBuilder.AlterColumn<int>(
                name: "StateInfoId",
                table: "AkhCityInfo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhCityInfo_Code_StateInfoId",
                table: "AkhCityInfo",
                columns: new[] { "Code", "StateInfoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhCityInfo_Name_StateInfoId",
                table: "AkhCityInfo",
                columns: new[] { "Name", "StateInfoId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhCityInfo_AkhStateInfo_StateInfoId",
                table: "AkhCityInfo",
                column: "StateInfoId",
                principalTable: "AkhStateInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
