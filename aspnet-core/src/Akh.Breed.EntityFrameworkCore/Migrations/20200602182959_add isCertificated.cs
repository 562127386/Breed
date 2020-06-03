using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class addisCertificated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CertificateDate",
                table: "AkhHerds",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCertificated",
                table: "AkhHerds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AkhSupportStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhSupportStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhSupportTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhSupportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AkhSupport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    Response = table.Column<string>(nullable: true),
                    SupportTypeId = table.Column<int>(nullable: true),
                    SupportStateId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhSupport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhSupport_AkhSupportStates_SupportStateId",
                        column: x => x.SupportStateId,
                        principalTable: "AkhSupportStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhSupport_AkhSupportTypes_SupportTypeId",
                        column: x => x.SupportTypeId,
                        principalTable: "AkhSupportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AkhSupport_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhSupport_SupportStateId",
                table: "AkhSupport",
                column: "SupportStateId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhSupport_SupportTypeId",
                table: "AkhSupport",
                column: "SupportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhSupport_UserId",
                table: "AkhSupport",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhSupport");

            migrationBuilder.DropTable(
                name: "AkhSupportStates");

            migrationBuilder.DropTable(
                name: "AkhSupportTypes");

            migrationBuilder.DropColumn(
                name: "CertificateDate",
                table: "AkhHerds");

            migrationBuilder.DropColumn(
                name: "IsCertificated",
                table: "AkhHerds");
        }
    }
}
