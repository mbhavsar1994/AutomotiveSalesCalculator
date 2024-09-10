using ABCAutomotive.WebAPI.Application.Commands;
using ABCAutomotive.WebAPI.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ABCAutomotive.WebAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/sales
    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var saleId = await _mediator.Send(command);
        return Ok(saleId);
    }

    // GET: api/sales
    [HttpGet]
    public async Task<IActionResult> GetSales()
    {
        var sales = await _mediator.Send(new GetSalesQuery());
        return Ok(sales);
    }
    
    [HttpGet("details")]
    public async Task<IActionResult> GetSaleDetails([FromQuery] DateTime sellingSeasonStart, [FromQuery] DateTime sellingSeasonEnd, [FromQuery] DateTime saleDate)
    {
        var query = new GetSaleDetailsQuery
        {
            SellingSeasonStart = sellingSeasonStart,
            SellingSeasonEnd = sellingSeasonEnd,
            SaleDate = saleDate
        };

        try
        {
            var result = await _mediator.Send(query);
            if (result == null || !result.Any())
            {
                return NotFound("No sales found within the given date range.");
            }
    
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
