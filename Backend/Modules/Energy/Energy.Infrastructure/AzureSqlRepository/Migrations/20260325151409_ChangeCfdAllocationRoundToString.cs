using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Energy.Infrastructure.AzureSqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCfdAllocationRoundToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CfdAllocationRound",
                table: "RenewableEnergyProjects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CfdAllocationRound",
                table: "RenewableEnergyProjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
