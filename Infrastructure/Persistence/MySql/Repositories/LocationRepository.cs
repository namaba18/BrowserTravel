using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Persistence.MySql.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _context;
        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Location?> GetByIdAsync(Guid id)
        {
            return await _context.Locations.FindAsync(id);
        }
    }
}
