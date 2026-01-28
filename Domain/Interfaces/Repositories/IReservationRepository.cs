using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task AddAsync(Reservation reservation);
        Task<Reservation?> GetByIdAsync(Guid id);
    }
}
