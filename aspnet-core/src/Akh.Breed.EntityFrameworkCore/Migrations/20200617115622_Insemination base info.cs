using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class Inseminationbaseinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkhAnomalyInfo",
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
                    table.PrimaryKey("PK_AkhAnomalyInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhBirthTypeInfo",
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
                    table.PrimaryKey("PK_AkhBirthTypeInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhBodyColorInfo",
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
                    table.PrimaryKey("PK_AkhBodyColorInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhBreedInfo",
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
                    table.PrimaryKey("PK_AkhBreedInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhMembershipInfo",
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
                    table.PrimaryKey("PK_AkhMembershipInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhSpotConnectorInfo",
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
                    table.PrimaryKey("PK_AkhSpotConnectorInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhAnomalyInfo_Code",
                table: "AkhAnomalyInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhAnomalyInfo_Name",
                table: "AkhAnomalyInfo",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhBirthTypeInfo_Code",
                table: "AkhBirthTypeInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhBirthTypeInfo_Name",
                table: "AkhBirthTypeInfo",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhBodyColorInfo_Code",
                table: "AkhBodyColorInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhBodyColorInfo_Name",
                table: "AkhBodyColorInfo",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhBreedInfo_Code",
                table: "AkhBreedInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhBreedInfo_Name",
                table: "AkhBreedInfo",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhMembershipInfo_Code",
                table: "AkhMembershipInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhMembershipInfo_Name",
                table: "AkhMembershipInfo",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhSpotConnectorInfo_Code",
                table: "AkhSpotConnectorInfo",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AkhSpotConnectorInfo_Name",
                table: "AkhSpotConnectorInfo",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhAnomalyInfo");

            migrationBuilder.DropTable(
                name: "AkhBirthTypeInfo");

            migrationBuilder.DropTable(
                name: "AkhBodyColorInfo");

            migrationBuilder.DropTable(
                name: "AkhBreedInfo");

            migrationBuilder.DropTable(
                name: "AkhMembershipInfo");

            migrationBuilder.DropTable(
                name: "AkhSpotConnectorInfo");
        }
    }
}
