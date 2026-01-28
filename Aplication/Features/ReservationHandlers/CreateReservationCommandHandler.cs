using Aplication.DTOs;
using Aplication.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.ValueObjects;


namespace Aplication.Features.ReservationHandlers
{
    public class CreateReservationCommandHandler
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

        public async Task Handle(CreateReservationCommand request)
        {
            Car? car = await _carRepository.GetByIdAsync(request.CarId)??throw new DirectoryNotFoundException("Car not found");
            Customer? customer = await _customerRepository.GetByIdAsync(request.CustomerId)??throw new DirectoryNotFoundException("Customer not found");
            Location? pickUpLocation = await _locationRepository.GetByIdAsync(request.PickUpLocationId)??throw new DirectoryNotFoundException("Pick-up location not found");
            Location? dropOffLocation = await _locationRepository.GetByIdAsync(request.DropOffLocationId)??throw new DirectoryNotFoundException("Drop-off location not found");
            DateRange dateRange = new(request.Start, request.End);
            
            var reservation = new Reservation(car, customer, dateRange, pickUpLocation, dropOffLocation);

            await _reservationRepository.AddAsync(reservation);
            
            foreach (var domainEvent in reservation.DomainEvents)
            {
                await _eventDispatcher.DispatchAsync(domainEvent);
            }

            reservation.ClearDomainEvents();
        }
    }
}
