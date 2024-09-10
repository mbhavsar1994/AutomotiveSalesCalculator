namespace ABCAutomotive.WebAPI.Core.Dtos;

public class SaleDetailsDto
{
    public Guid SaleId { get; set; }
    public string CarMake { get; set; }
    public string CarModel { get; set; }
    public decimal Profit { get; set; }
    public decimal Commission { get; set; }
}