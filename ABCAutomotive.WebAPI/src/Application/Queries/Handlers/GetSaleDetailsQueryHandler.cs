using ABCAutomotive.WebAPI.Application.Services;
using ABCAutomotive.WebAPI.Core.Dtos;
using ABCAutomotive.WebAPI.Core.Interfaces;
using MediatR;

namespace ABCAutomotive.WebAPI.Application.Queries.Handlers;

public class GetSaleDetailsQueryHandler : IRequestHandler<GetSaleDetailsQuery, List<SaleDetailsDto>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly CarPriceAdjustmentServiceFactory _priceAdjustmentServiceFactory;

    public GetSaleDetailsQueryHandler(ISaleRepository saleRepository, CarPriceAdjustmentServiceFactory priceAdjustmentServiceFactory)
    {
        _saleRepository = saleRepository;
        _priceAdjustmentServiceFactory = priceAdjustmentServiceFactory;
    }

    public async Task<List<SaleDetailsDto>> Handle(GetSaleDetailsQuery request, CancellationToken cancellationToken)
    {
        // Validate the date range
        ValidateDates(request.SellingSeasonStart, request.SellingSeasonEnd);

        // Retrieve sales based on the sale date and selling season range
        var sales = await _saleRepository.GetSalesWithinDateRangeAsync(request.SellingSeasonStart, request.SellingSeasonEnd, request.SaleDate);

        if (sales == null || !sales.Any())
        {
            return new List<SaleDetailsDto>();
        }

        var saleDetailsList = new List<SaleDetailsDto>();

        foreach (var sale in sales)
        {
            var car = sale.Car;

            // Get the appropriate price adjustment service based on car type
            var priceAdjustmentService = _priceAdjustmentServiceFactory.GetCarPriceAdjustmentService(car);

            // Calculate dealership profit using the selected service
            decimal profit = priceAdjustmentService.AdjustPrice(car, request.SellingSeasonStart, request.SellingSeasonEnd, sale.SaleDate) - car.Cost;
            decimal commission = CalculateCommission(profit);

            // Add the calculated result to the list
            saleDetailsList.Add(new SaleDetailsDto
            {
                SaleId = sale.SaleId,
                CarMake = car.Make,
                CarModel = car.Model,
                Profit = profit,
                Commission = commission
            });
        }

        return saleDetailsList;
    }

    private decimal CalculateCommission(decimal profit)
    {
        return profit * 0.0125m; // 1.25% commission
    }
    
    private void ValidateDates(DateTime sellingSeasonStart, DateTime sellingSeasonEnd)
    {
        // Ensure SellingSeasonStart is before SellingSeasonEnd
        if (sellingSeasonStart >= sellingSeasonEnd)
        {
            throw new ArgumentException("The selling season start date must be earlier than the selling season end date.");
        }
    }
}
