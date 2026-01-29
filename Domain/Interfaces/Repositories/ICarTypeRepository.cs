using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface ICarTypeRepository
    {
        Task<List<CarType>> GetAsync();
    }
}
