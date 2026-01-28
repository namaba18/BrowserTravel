using Aplication.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.ValueObjects;
using MediatR;

namespace Aplication.Modules.Reservation
{
    public class CreateReservationCommand : IRequest<bool>
    {
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid PickUpLocationId { get; set; }
        public Guid DropOffLocationId { get; set; }
        public string? Note { get; set; }
    }

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, bool>
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ICarRepository _carRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILocationRepository _locationRepository;
        public CreateReservationCommandHandler(IEventDispatcher eventDispatcher, ICarRepository carRepository, IReservationRepository reservationRepository, ICustomerRepository customerRepository, ILocationRepository locationRepository)
        {
            _eventDispatcher = eventDispatcher;
            _carRepository = carRepository;
            _reservationRepository=reservationRepository;
            _customerRepository=customerRepository;
            _locationRepository=locationRepository;
        }

        public async Task<bool> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Car? car = await _carRepository.GetByIdAsync(request.CarId)??throw new DirectoryNotFoundException("Car not found");
            Customer? customer = await _customerRepository.GetByIdAsync(request.CustomerId)??throw new DirectoryNotFoundException("Customer not found");
            Location? pickUpLocation = await _locationRepository.GetByIdAsync(request.PickUpLocationId)??throw new DirectoryNotFoundException("Pick-up location not found");
            Location? dropOffLocation = await _locationRepository.GetByIdAsync(request.DropOffLocationId)??throw new DirectoryNotFoundException("Drop-off location not found");
            DateRange dateRange = new(request.Start, request.End);

            var reservation = new Domain.Entities.Reservation(car, customer, dateRange, pickUpLocation, dropOffLocation);

            await _reservationRepository.AddAsync(reservation);

            foreach (var domainEvent in reservation.DomainEvents)
            {
                await _eventDispatcher.DispatchAsync(domainEvent);
            }

            reservation.ClearDomainEvents();

            return true;
        }
    }
}
