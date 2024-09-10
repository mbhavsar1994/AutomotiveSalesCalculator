using ABCAutomotive.WebAPI.Core.Entities;

namespace ABCAutomotive.WebAPI.Core.Interfaces;

public interface ICarPriceAdjustmentService
{
    decimal AdjustPrice(Car car, DateTime sellingSeasonStart, DateTime sellingSeasonEnd, DateTime saleDate);
}