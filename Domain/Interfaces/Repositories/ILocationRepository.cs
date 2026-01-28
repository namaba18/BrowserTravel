using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ILocationRepository
    {
        Task<Location?> GetByIdAsync(Guid id);

    }
}
