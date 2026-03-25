using Energy.Domain.Models;

namespace Energy.Domain.Interfaces;

public interface IFuelHalfHourRepository
{
    Task UpsertAsync(IEnumerable<FuelHalfHour> rows, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FuelHalfHour>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FuelHalfHour>> GetLatestAsync(CancellationToken cancellationToken = default);
}
