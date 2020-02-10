using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class changestateinfotable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhCityInfo_AkhStateInfo_StateInfoId",
                table: "AkhCityInfo");

            migrationBuilder.AlterColumn<int>(
                name: "StateInfoId",
                table: "AkhCityInfo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AkhCityInfo_AkhStateInfo_StateInfoId",
                table: "AkhCityInfo",
                column: "StateInfoId",
                principalTable: "AkhStateInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhCityInfo_AkhStateInfo_StateInfoId",
                table: "AkhCityInfo");

            migrationBuilder.AlterColumn<int>(
                name: "StateInfoId",
                table: "AkhCityInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AkhCityInfo_AkhStateInfo_StateInfoId",
                table: "AkhCityInfo",
                column: "StateInfoId",
                principalTable: "AkhStateInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
