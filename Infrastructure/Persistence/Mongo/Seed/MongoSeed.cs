
using Infrastructure.Persistence.Mongo.Models;
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
            var collection = _context.SearchCar;

            if (await collection.CountDocumentsAsync(FilterDefinition<SearchCar>.Empty) == 0)
            {
                var initialData = new List<SearchCar>
                {
                    new() {
                        Id = Guid.NewGuid(),
                        Name = "Sedan",
                        Description = "A sedan car type"
                    },
                    new() {
                        Id = Guid.NewGuid(),
                        Name = "SUV",
                        Description = "A sport utility vehicle car type"
                    },
                    new() {
                        Id = Guid.NewGuid(),
                        Name = "Hatchback",
                        Description = "A hatchback car type"
                    }
                };
                if (initialData.Count > 0)
                {
                    await collection.InsertManyAsync(initialData);
                }
            }
        }
    }
}
