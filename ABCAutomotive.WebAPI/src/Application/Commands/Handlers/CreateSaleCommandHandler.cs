using ABCAutomotive.WebAPI.Core.Entities;
using ABCAutomotive.WebAPI.Core.Interfaces;
using ABCAutomotive.WebAPI.Infrastructure.Persistence;
using MediatR;

namespace ABCAutomotive.WebAPI.Application.Commands.Handlers;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
{
    private readonly ICarRepository _carRepository;
    private readonly ISaleRepository _saleRepository;

    public CreateSaleCommandHandler(ICarRepository carRepository, ISaleRepository saleRepository)
    {
        _carRepository = carRepository;
        _saleRepository = saleRepository;
    }

    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetByIdAsync(request.CarId);
        if (car == null) throw new Exception("Car not found");

        // Create a sale entity with the car and sale date
        var sale = new Sale
        {
            Car = car,
            SaleDate = request.SaleDate
        };

        // Store the sale record, but without storing SellingSeasonStart/End
        await _saleRepository.AddAsync(sale);

        // Return the saleId
        return sale.SaleId;
    }
}