using Domain.Interfaces.Repositories;
using Domain.Models;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Mongo.Repositories
{
    public class CarTypeRepository : ICarTypeRepository
    {
        private readonly MongoContext _context;

        public CarTypeRepository(MongoContext context)
        {
            _context = context;
        }

        public async Task<CarType?> GetByIdAsync(string id)
        {
            return await _context.CarTypes.Find(ct => ct.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveAsync(CarType carType)
        {
            await _context.CarTypes.ReplaceOneAsync(
                ct => ct.Id == carType.Id,
                carType,
                new ReplaceOptions { IsUpsert = true });
        }
    }
}
