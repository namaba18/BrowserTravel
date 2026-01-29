using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ICarRepository
    {
        Task<Car?> GetByIdAsync(Guid id);
        Task<List<Car>> GetAvailableCarsAsync(Guid locationId, DateTime start, DateTime end);
        Task AddAsync(Car car);
    }
}
