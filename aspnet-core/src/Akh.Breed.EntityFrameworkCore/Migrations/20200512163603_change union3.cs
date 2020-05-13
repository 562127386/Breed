using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class changeunion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkhUnionEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Family = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Post = table.Column<string>(nullable: true),
                    UnionInfoId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhUnionEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhUnionEmployees_AkhUnionInfo_UnionInfoId",
                        column: x => x.UnionInfoId,
                        principalTable: "AkhUnionInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhUnionEmployees_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhUnionEmployees_UnionInfoId",
                table: "AkhUnionEmployees",
                column: "UnionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhUnionEmployees_UserId",
                table: "AkhUnionEmployees",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhUnionEmployees");
        }
    }
}
