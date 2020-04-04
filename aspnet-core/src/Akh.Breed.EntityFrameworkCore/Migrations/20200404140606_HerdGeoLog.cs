using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class HerdGeoLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkhHerdGeoLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    HerdId = table.Column<int>(nullable: true),
                    OfficerId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhHerdGeoLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhHerdGeoLogs_AkhHerds_HerdId",
                        column: x => x.HerdId,
                        principalTable: "AkhHerds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhHerdGeoLogs_AkhOfficers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "AkhOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerdGeoLogs_HerdId",
                table: "AkhHerdGeoLogs",
                column: "HerdId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhHerdGeoLogs_OfficerId",
                table: "AkhHerdGeoLogs",
                column: "OfficerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhHerdGeoLogs");
        }
    }
}
