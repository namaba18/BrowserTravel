using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface ISearchCarRepository
    {
        Task<SearchCar?> GetAsync(Guid locationId, DateTime start, DateTime end, CancellationToken cancellationToken);
        Task SaveAsync(SearchCar search);
    }
}
