using ABCAutomotive.WebAPI.Core.Entities;
using ABCAutomotive.WebAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ABCAutomotive.WebAPI.Infrastructure.Persistence.Repositories;

public class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _context;

    public CarRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Car> GetByIdAsync(Guid id)
    {
        return await _context.Cars.FindAsync(id);
    }

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task AddAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
    }
}