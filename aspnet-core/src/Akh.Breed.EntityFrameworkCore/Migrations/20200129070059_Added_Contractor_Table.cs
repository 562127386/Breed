using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akh.Breed.Migrations
{
    public partial class Added_Contractor_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkhContractors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Institution = table.Column<string>(nullable: true),
                    SubInstitution = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    NationalCode = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Family = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirmType = table.Column<int>(nullable: false),
                    FirmName = table.Column<string>(nullable: true),
                    FirmRegNumber = table.Column<string>(nullable: true),
                    FirmEstablishmentYear = table.Column<string>(nullable: true),
                    FullTimeStaffDiploma = table.Column<int>(nullable: false),
                    FullTimeStaffAssociateDegree = table.Column<int>(nullable: false),
                    FullTimeStaffBachelorAndUpper = table.Column<int>(nullable: false),
                    PartialTimeStaffDiploma = table.Column<int>(nullable: false),
                    PartialTimeStaffAssociateDegree = table.Column<int>(nullable: false),
                    PartialTimeStaffBachelorAndUpper = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkhContractors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkhContractors");
        }
    }
}
