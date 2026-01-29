using Domain.Models;
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
            await SeedCarType();
        }

        private async Task SeedCarType()
        {
            var collection = _context.CarTypes;

            if (await collection.CountDocumentsAsync(FilterDefinition<CarType>.Empty) == 0)
            {
                var initialData = new List<CarType>() 
                { 
                    new() 
                    { 
                        Id = Guid.NewGuid().ToString(), 
                        Name = "Suv", 
                        Description="A hybrid vehicle between a car and a truck, combining the height and traction of a 4x4 with the comfort and handling of a car." 
                    },
                    new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Sedan",
                        Description="Standard passenger car, easily recognizable by its three-box body."
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
