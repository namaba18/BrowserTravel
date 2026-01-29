using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface ICarTypeRepository
    {
        Task<CarType?> GetByIdAsync(string id);
    }
}
