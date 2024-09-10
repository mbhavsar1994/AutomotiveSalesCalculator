using ABCAutomotive.WebAPI.Core.Enums;

namespace ABCAutomotive.WebAPI.Core.Entities;

public class Car
{
    public Guid CarId { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public decimal Cost { get; set; }
    public decimal MSRP { get; set; }
    public CarType CarType { get; set; }
    
    public ICollection<Sale> Sales { get; set; }
}