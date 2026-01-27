
using MongoDB.Driver;

namespace Infrastructure.Persistence.Mongo.Seed
{
    public class MongoSeed
    {
        private readonly MongoContext _context;

        public MongoSeed(MongoContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await SeedCarAvailability();
        }

        private async Task SeedCarAvailability()
        {
            var collection = _context.CarAvailability;

            if (await collection.CountDocumentsAsync(FilterDefinition<Models.CarAvailability>.Empty) == 0)
            {
                var initialData = new List<Models.CarAvailability>
                {
                    // Add initial CarAvailability data here
                };
                if (initialData.Count > 0)
                {
                    await collection.InsertManyAsync(initialData);
                }
            }
        }
    }
}
