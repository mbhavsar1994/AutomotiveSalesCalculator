using ABCAutomotive.WebAPI.Core.Entities;
using ABCAutomotive.WebAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ABCAutomotive.WebAPI.Infrastructure.Persistence.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly ApplicationDbContext _context;

    public SaleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await _context.Sales.Include(s => s.Car)
            .FirstOrDefaultAsync(s => s.SaleId == id);
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales.Include(s => s.Car).ToListAsync();
    }

    public async Task AddAsync(Sale sale)
    {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Sale>> GetSalesWithinDateRangeAsync(DateTime sellingSeasonStart, DateTime sellingSeasonEnd, DateTime saleDate)
    {
        return await _context.Sales
            .Include(s => s.Car)
            .Where(s => s.SaleDate == saleDate)
            .ToListAsync();
    }
}