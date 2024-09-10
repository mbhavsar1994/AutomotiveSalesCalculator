using ABCAutomotive.WebAPI.Core.Entities;

namespace ABCAutomotive.WebAPI.Core.Interfaces;

public interface ICarRepository
{
    Task<Car> GetByIdAsync(Guid id);
    Task<IEnumerable<Car>> GetAllAsync();
    Task AddAsync(Car car);
}