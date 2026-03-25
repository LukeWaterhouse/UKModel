using Energy.Infrastructure.AzureSqlRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Energy.Infrastructure.AzureSqlRepository;

public class EnergyDbContext(DbContextOptions<EnergyDbContext> options) : DbContext(options)
{
    public DbSet<RenewableEnergyProjectDb> RenewableEnergyProjects => Set<RenewableEnergyProjectDb>();
    public DbSet<FuelHalfHourDb> FuelHalfHours => Set<FuelHalfHourDb>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RenewableEnergyProjectDb>(entity =>
        {
            entity.ToTable("RenewableEnergyProjects");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.RefId).IsUnique();
            entity.Property(e => e.InstalledCapacityMWe).HasColumnType("decimal(18,4)");
            entity.Property(e => e.RoBandingRocPerMWh).HasColumnType("decimal(18,4)");
            entity.Property(e => e.FitTariffPencePerKWh).HasColumnType("decimal(18,4)");
            entity.Property(e => e.CfdCapacityMW).HasColumnType("decimal(18,4)");
            entity.Property(e => e.TurbineCapacityMW).HasColumnType("decimal(18,4)");
            entity.Property(e => e.HeightOfTurbinesMetres).HasColumnType("decimal(18,4)");
            entity.Property(e => e.SolarSiteAreaSqm).HasColumnType("decimal(18,4)");
            entity.Property(e => e.XCoordinate).HasColumnType("decimal(18,4)");
            entity.Property(e => e.YCoordinate).HasColumnType("decimal(18,4)");
            entity.Property(e => e.Latitude).HasColumnType("float");
            entity.Property(e => e.Longitude).HasColumnType("float");
        });

        modelBuilder.Entity<FuelHalfHourDb>(entity =>
        {
            entity.ToTable("FuelHalfHours");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.StartTimeUtc, e.FuelType }).IsUnique();
            entity.HasIndex(e => e.SettlementDate);
            entity.Property(e => e.FuelType).HasMaxLength(20);
            entity.Property(e => e.SourceDataset).HasMaxLength(20);
        });
    }
}
