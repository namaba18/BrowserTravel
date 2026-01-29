using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.MySql.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;
        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
        }

        public async Task<List<Car>> GetAvailableCarsAsync(Guid locationId, DateTime start, DateTime end)
        {
            return await _context.Cars
                .Include(c => c.Reservations)
                .Where(c => c.Location.Id == locationId && c.Status == CarStatus.Available)
                .Where(c => !c.Reservations.Any(r => r.DateRange.StartDate < end && r.DateRange.EndDate > start))
                .ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(Guid id)
        {
            return await _context.Cars.Include(c => c.Reservations).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
