using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Energy.Infrastructure.AzureSqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class AddFuelHalfHoursTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuelHalfHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuelType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GenerationMw = table.Column<int>(type: "int", nullable: false),
                    PublishTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SettlementDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SettlementPeriod = table.Column<int>(type: "int", nullable: false),
                    SourceDataset = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelHalfHours", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuelHalfHours_SettlementDate",
                table: "FuelHalfHours",
                column: "SettlementDate");

            migrationBuilder.CreateIndex(
                name: "IX_FuelHalfHours_StartTimeUtc_FuelType",
                table: "FuelHalfHours",
                columns: new[] { "StartTimeUtc", "FuelType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelHalfHours");
        }
    }
}
