using Infrastructure.Persistence.Mongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Mongo
{
    public class MongoContext
    {

        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(options.Value.MongoDatabase);
        }

        public IMongoCollection<CarType> CarType =>
            _database.GetCollection<CarType>("carType");
    }
}
