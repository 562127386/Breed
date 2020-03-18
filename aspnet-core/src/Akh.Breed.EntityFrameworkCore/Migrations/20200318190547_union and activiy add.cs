using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class unionandactiviyadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkhActivityInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhActivityInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhUnionInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhUnionInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhActivityInfo_Code",
                table: "AkhActivityInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhActivityInfo_Name",
                table: "AkhActivityInfo",
                column: "Name",
                unique: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhActivityInfo");

            migrationBuilder.DropTable(
                name: "AkhUnionInfo");
        }
    }
}
