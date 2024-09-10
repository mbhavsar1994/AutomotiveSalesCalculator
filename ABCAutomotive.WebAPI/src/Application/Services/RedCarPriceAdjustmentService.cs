using ABCAutomotive.WebAPI.Core.Entities;
using ABCAutomotive.WebAPI.Core.Interfaces;

namespace ABCAutomotive.WebAPI.Application.Services;

public class RedCarPriceAdjustmentService : ICarPriceAdjustmentService
{
    public decimal AdjustPrice(Car car, DateTime sellingSeasonStart, DateTime sellingSeasonEnd, DateTime saleDate)
    {
        // If the sale date is within the selling season, return full MSRP
        if (saleDate >= sellingSeasonStart && saleDate <= sellingSeasonEnd)
        {
            return car.MSRP;
        }
        
        var basePrice = car.MSRP;
        var weeksAfterSellingSeason = (saleDate - sellingSeasonEnd).TotalDays / 7;
        return Math.Max(basePrice - (basePrice * 0.02m * (decimal)weeksAfterSellingSeason), car.Cost);
    }
}