using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class editContractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirmType",
                table: "AkhContractors");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AkhContractors",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AkhContractors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirmTypeId",
                table: "AkhContractors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AkhContractors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhContractors_FirmTypeId",
                table: "AkhContractors",
                column: "FirmTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AkhContractors_AkhFirmType_FirmTypeId",
                table: "AkhContractors",
                column: "FirmTypeId",
                principalTable: "AkhFirmType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AkhContractors_AkhFirmType_FirmTypeId",
                table: "AkhContractors");

            migrationBuilder.DropIndex(
                name: "IX_AkhContractors_FirmTypeId",
                table: "AkhContractors");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "AkhContractors");

            migrationBuilder.DropColumn(
                name: "FirmTypeId",
                table: "AkhContractors");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AkhContractors");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AkhContractors",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "FirmType",
                table: "AkhContractors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
