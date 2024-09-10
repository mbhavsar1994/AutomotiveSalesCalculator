using ABCAutomotive.WebAPI.Core.Entities;
using ABCAutomotive.WebAPI.Core.Enums;
using ABCAutomotive.WebAPI.Core.Interfaces;

namespace ABCAutomotive.WebAPI.Application.Services;

public class CarPriceAdjustmentServiceFactory
{
    private readonly ICarPriceAdjustmentService _regularCarPriceAdjustmentService;
    private readonly ICarPriceAdjustmentService _suvPriceAdjustmentService;
    private readonly ICarPriceAdjustmentService _convertiblePriceAdjustmentService;
    private readonly ICarPriceAdjustmentService _redCarPriceAdjustmentService;
    
    public CarPriceAdjustmentServiceFactory(
        RegularCarPriceAdjustmentService regularCarPriceAdjustmentService,
        SuvPriceAdjustmentService suvPriceAdjustmentService,
        ConvertiblePriceAdjustmentService convertiblePriceAdjustmentService,
        RedCarPriceAdjustmentService redCarPriceAdjustmentService)
    {
        _regularCarPriceAdjustmentService = regularCarPriceAdjustmentService;
        _suvPriceAdjustmentService = suvPriceAdjustmentService;
        _convertiblePriceAdjustmentService = convertiblePriceAdjustmentService;
        _redCarPriceAdjustmentService = redCarPriceAdjustmentService;
    }
    public ICarPriceAdjustmentService GetCarPriceAdjustmentService(Car car)
    {
        if (car.Color.ToLower() == "red")
        {
            return _redCarPriceAdjustmentService;
        }
        
        return car.CarType switch
        {
            CarType.SUV => _suvPriceAdjustmentService,
            CarType.Convertible => _convertiblePriceAdjustmentService,
            _ => _regularCarPriceAdjustmentService // Default to regular car adjustment
        };
    }

}