using ABCAutomotive.WebAPI.Core.Entities;
using ABCAutomotive.WebAPI.Core.Interfaces;

namespace ABCAutomotive.WebAPI.Application.Services;

public class ConvertiblePriceAdjustmentService : ICarPriceAdjustmentService
{
    public decimal AdjustPrice(Car car, DateTime sellingSeasonStart, DateTime sellingSeasonEnd, DateTime saleDate)
    {
        // Convertibles don't change in price, regardless of the sale date or season
        return car.MSRP;
    }
}