using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Energy.Infrastructure.AzureSqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RenewableEnergyProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldRefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefId = table.Column<int>(type: "int", nullable: false),
                    RecordLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnologyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageCoLocationRepdRefId = table.Column<int>(type: "int", nullable: true),
                    InstalledCapacityMWe = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    ShareCommunityScheme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChpEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CfdAllocationRound = table.Column<int>(type: "int", nullable: true),
                    RoBandingRocPerMWh = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    FitTariffPencePerKWh = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    CfdCapacityMW = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    TurbineCapacityMW = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    NumberOfTurbines = table.Column<int>(type: "int", nullable: true),
                    HeightOfTurbinesMetres = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    MountingTypeForSolar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DevelopmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DevelopmentStatusShort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReApplyingNewRepdRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReApplyingOldRepdRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XCoordinate = table.Column<int>(type: "int", nullable: true),
                    YCoordinate = table.Column<int>(type: "int", nullable: true),
                    PlanningAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanningApplicationReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppealReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecretaryOfStateReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfSecretaryOfStateIntervention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JudicialReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OffshoreWindRound = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanningApplicationSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanningApplicationWithdrawn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanningPermissionRefused = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppealLodged = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppealWithdrawn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppealRefused = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppealGranted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanningPermissionGranted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecretaryOfStateIntervened = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecretaryOfStateRefusal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecretaryOfStateGranted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanningPermissionExpired = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnderConstruction = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Operational = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HeatNetworkRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolarSiteAreaSqm = table.Column<decimal>(type: "decimal(18,4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenewableEnergyProjects", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RenewableEnergyProjects_RefId",
                table: "RenewableEnergyProjects",
                column: "RefId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RenewableEnergyProjects");
        }
    }
}
