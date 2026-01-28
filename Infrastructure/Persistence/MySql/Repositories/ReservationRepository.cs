using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.MySql.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task<Reservation?> GetByIdAsync(Guid id)
        {
            return await _context.Reservations.Include(r => r.Car).FirstOrDefaultAsync(r => r.Id == id);
        }


    }
}
