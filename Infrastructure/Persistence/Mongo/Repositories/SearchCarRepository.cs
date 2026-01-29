using Domain.Interfaces.Repositories;
using Domain.Models;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Mongo.Repositories
{
    public class SearchCarRepository : ISearchCarRepository
    {
        private readonly MongoContext _context;

        public SearchCarRepository(MongoContext context)
        {
           _context = context;
        }

        public async Task<SearchCar?> GetAsync(Guid locationId, DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            return await _context.SearchCar.Find(x =>
                x.LocationId == locationId.ToString() &&
                x.Start == start &&
                x.End == end)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task SaveAsync(SearchCar search)
        {
            await _context.SearchCar.ReplaceOneAsync(
                x => x.Id == search.Id,
                search,
                new ReplaceOptions { IsUpsert = true });
        }
    }
}
