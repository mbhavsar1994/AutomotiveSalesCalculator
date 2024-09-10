namespace ABCAutomotive.WebAPI.Core.Entities;

public class Sale
{
    public Guid SaleId { get; set; }
    
    public Guid CarId { get; set; }
    public Car Car { get; set; }
    public DateTime SaleDate { get; set; }
}