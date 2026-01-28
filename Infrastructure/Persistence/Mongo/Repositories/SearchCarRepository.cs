using Infrastructure.Persistence.Mongo.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Mongo.Repositories
{
    public class SearchCarRepository
    {
        private readonly IMongoCollection<SearchCar> _collection;

        public SearchCarRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<SearchCar>("search_cars");
        }

        public async Task<SearchCar?> GetAsync(
        Guid locationId,
        DateTime start,
        DateTime end)
        {
            return await _collection.Find(x =>
                x.LocationId == locationId &&
                x.Start == start &&
                x.End == end)
                .FirstOrDefaultAsync();
        }

        public async Task SaveAsync(SearchCar search)
        {
            await _collection.ReplaceOneAsync(
                x => x.Id == search.Id,
                search,
                new ReplaceOptions { IsUpsert = true });
        }
    }
}
