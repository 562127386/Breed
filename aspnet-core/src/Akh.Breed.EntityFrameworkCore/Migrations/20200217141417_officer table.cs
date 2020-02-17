using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class officertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkhOfficers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    NationalCode = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Family = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    IdNo = table.Column<string>(nullable: true),
                    WorkNumber = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    AcademicDegreeId = table.Column<int>(nullable: false),
                    StateInfoId = table.Column<int>(nullable: false),
                    ContractorId = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhOfficers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkhOfficers_AkhAcademicDegree_AcademicDegreeId",
                        column: x => x.AcademicDegreeId,
                        principalTable: "AkhAcademicDegree",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AkhOfficers_AkhContractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "AkhContractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AkhOfficers_AkhStateInfo_StateInfoId",
                        column: x => x.StateInfoId,
                        principalTable: "AkhStateInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkhOfficers_AcademicDegreeId",
                table: "AkhOfficers",
                column: "AcademicDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhOfficers_ContractorId",
                table: "AkhOfficers",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_AkhOfficers_StateInfoId",
                table: "AkhOfficers",
                column: "StateInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhOfficers");
        }
    }
}
