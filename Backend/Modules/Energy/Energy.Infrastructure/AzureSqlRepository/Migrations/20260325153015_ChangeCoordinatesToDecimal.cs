using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Energy.Infrastructure.AzureSqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCoordinatesToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "YCoordinate",
                table: "RenewableEnergyProjects",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "XCoordinate",
                table: "RenewableEnergyProjects",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "YCoordinate",
                table: "RenewableEnergyProjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "XCoordinate",
                table: "RenewableEnergyProjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);
        }
    }
}
