namespace ABCAutomotive.Client.DataAccess;

public interface ISalesService
{
    Task<List<SaleDetailsDto>> GetSaleDetailsAsync(DateTime sellingSeasonStart, DateTime sellingSeasonEnd, DateTime saleDate);
}