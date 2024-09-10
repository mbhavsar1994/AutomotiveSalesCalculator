using ABCAutomotive.WebAPI.Core.Entities;

namespace ABCAutomotive.WebAPI.Core.Interfaces;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(Guid id);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task AddAsync(Sale? sale);
    
    Task<IEnumerable<Sale>> GetSalesWithinDateRangeAsync(DateTime sellingSeasonStart, DateTime sellingSeasonEnd, DateTime saleDate);
}