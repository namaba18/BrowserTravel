using Domain.Interfaces.Repositories;
using Domain.Models;
using MongoDB.Bson;
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

        public async Task<List<CarType>> GetAsync()
        {
            return await _context.CarTypes.Find(new BsonDocument()).ToListAsync();
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
