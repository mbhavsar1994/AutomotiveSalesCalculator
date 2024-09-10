using MediatR;

namespace ABCAutomotive.WebAPI.Application.Commands;

public class CreateSaleCommand : IRequest<Guid> // Returns SaleId
{
    public Guid CarId { get; set; }
    public DateTime SaleDate { get; set; }
}                                      