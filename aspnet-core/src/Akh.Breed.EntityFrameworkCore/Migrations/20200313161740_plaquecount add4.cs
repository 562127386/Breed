using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class plaquecountadd4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ToCode",
                table: "AkhPlaqueStores",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<long>(
                name: "FromCode",
                table: "AkhPlaqueStores",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<long>(
                name: "ToCode",
                table: "AkhPlaqueOfficers",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<long>(
                name: "FromCode",
                table: "AkhPlaqueOfficers",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<long>(
                name: "Code",
                table: "AkhPlaqueInfos",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ToCode",
                table: "AkhPlaqueStores",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(long),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "FromCode",
                table: "AkhPlaqueStores",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(long),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "ToCode",
                table: "AkhPlaqueOfficers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(long),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "FromCode",
                table: "AkhPlaqueOfficers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(long),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AkhPlaqueInfos",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(long),
                oldMaxLength: 15);
        }
    }
}
