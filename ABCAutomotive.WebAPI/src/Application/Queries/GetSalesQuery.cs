using ABCAutomotive.WebAPI.Core.Entities;
using MediatR;

namespace ABCAutomotive.WebAPI.Application.Queries;

public class GetSalesQuery : IRequest<IEnumerable<Sale>>
{
}