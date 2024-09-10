using ABCAutomotive.WebAPI.Core.Dtos;
using MediatR;

namespace ABCAutomotive.WebAPI.Application.Queries;

public class GetSaleDetailsQuery : IRequest<List<SaleDetailsDto>>
{
    public DateTime SellingSeasonStart { get; set; }
    public DateTime SellingSeasonEnd { get; set; }
    public DateTime SaleDate { get; set; }
}
