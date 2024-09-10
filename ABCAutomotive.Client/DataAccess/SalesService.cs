using System.Net.Http.Json;

namespace ABCAutomotive.Client.DataAccess;

public class SalesService : ISalesService
{
    private readonly HttpClient _httpClient;
    public SalesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<SaleDetailsDto>> GetSaleDetailsAsync(DateTime sellingSeasonStart, DateTime sellingSeasonEnd, DateTime saleDate)
    {
        try
        {
            string url = $"api/sales/details?sellingSeasonStart={sellingSeasonStart:yyyy-MM-dd}&sellingSeasonEnd={sellingSeasonEnd:yyyy-MM-dd}&saleDate={saleDate:yyyy-MM-dd}";
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<SaleDetailsDto>();
            }
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<SaleDetailsDto>>();
            return result ??  new List<SaleDetailsDto>();
        }
        catch (HttpRequestException ex)
        {
            // Handle connection errors
            throw new ApplicationException(
                "There was an issue connecting to the server. Please check your network connection or try again later.", ex);
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            throw new ApplicationException("An unexpected error occurred. Please try again later.", ex);
        }
    }
}