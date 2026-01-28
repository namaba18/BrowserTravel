using Aplication.DTOs;
using Aplication.Interfaces;
using Domain.Entities;


namespace Aplication.Features.ReservationHandlers
{
    public class CreateReservationCommandHandler
    {
        private readonly IEventDispatcher _eventDispatcher;
        public CreateReservationCommandHandler(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public async Task Handle(CreateReservationCommand request)
        {
            var reservation = new Reservation();

            // Crear la reserva con los datos del comando
            foreach (var domainEvent in reservation.DomainEvents)
            {
                await _eventDispatcher.DispatchAsync(domainEvent);
            }

            reservation.ClearDomainEvents();
        }
    }
}
