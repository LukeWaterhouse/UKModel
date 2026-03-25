using Energy.Infrastructure.AzureSqlRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Energy.Infrastructure.AzureSqlRepository;

public class EnergyDbContext(DbContextOptions<EnergyDbContext> options) : DbContext(options)
{
    public DbSet<RenewableEnergyProjectDb> RenewableEnergyProjects => Set<RenewableEnergyProjectDb>();

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
    }
}
