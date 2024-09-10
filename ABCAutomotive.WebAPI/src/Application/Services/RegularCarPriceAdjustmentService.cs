using ABCAutomotive.WebAPI.Core.Entities;
using ABCAutomotive.WebAPI.Core.Interfaces;

namespace ABCAutomotive.WebAPI.Application.Services;

public class RegularCarPriceAdjustmentService : ICarPriceAdjustmentService
{
    public decimal AdjustPrice(Car car, DateTime sellingSeasonStart, DateTime sellingSeasonEnd, DateTime saleDate)
    {
        // If the sale date is within the selling season, return full MSRP
        if (saleDate >= sellingSeasonStart && saleDate <= sellingSeasonEnd)
        {
            return car.MSRP;
        }

        // If the sale date is after the selling season, calculate price adjustment
        var basePrice = car.MSRP;
        var weeksAfterSellingSeason = (saleDate - sellingSeasonEnd).TotalDays / 7;

        decimal salePrice = basePrice - (basePrice * 0.04m * (decimal)weeksAfterSellingSeason);

        return Math.Max(salePrice, car.Cost); // Ensure no loss
    }
    
}