using ABCAutomotive.WebAPI.Core.Entities;
using ABCAutomotive.WebAPI.Core.Interfaces;
using MediatR;

namespace ABCAutomotive.WebAPI.Application.Queries.Handlers;

public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, IEnumerable<Sale>>
{
    private readonly ISaleRepository _saleRepository;

    public GetSalesQueryHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<IEnumerable<Sale>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        return await _saleRepository.GetAllAsync();
    }
}