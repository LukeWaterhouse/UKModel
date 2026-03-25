namespace Energy.Infrastructure.AzureSqlRepository.Models;

public class FuelHalfHourDb
{
    public int Id { get; set; }
    public DateTime StartTimeUtc { get; set; }
    public string FuelType { get; set; } = string.Empty;
    public int GenerationMw { get; set; }
    public DateTime PublishTimeUtc { get; set; }
    public DateOnly SettlementDate { get; set; }
    public int SettlementPeriod { get; set; }
    public string SourceDataset { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
}
