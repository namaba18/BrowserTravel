using Aplication.Interfaces;
using Aplication.Modules.Reservation;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces.Repositories;
using NSubstitute;

namespace UnitTests.Application.Modules.ReservationTests
{
    public class CreateReservationCommandHandlerTests
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ICarRepository _carRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly CreateReservationCommandHandler _handler;
        public CreateReservationCommandHandlerTests()
        {
            _eventDispatcher = Substitute.For<IEventDispatcher>();
            _carRepository = Substitute.For<ICarRepository>();
            _reservationRepository = Substitute.For<IReservationRepository>();
            _customerRepository = Substitute.For<ICustomerRepository>();
            _locationRepository = Substitute.For<ILocationRepository>();

            _handler = new CreateReservationCommandHandler(
                _eventDispatcher,
                _carRepository,
                _reservationRepository,
                _customerRepository,
                _locationRepository
            );
        }

        [Fact]
        public async Task Handle_ShouldCreateReservation_AndDispatchEvents()
        {
            Location location = new("Cundinamarca", "Bogotá", "Cr 33", "La 33");
            var carId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var pickUpId = Guid.NewGuid();
            var dropOffId = Guid.NewGuid();

            var car = new Car(location, "ABC123", "Toyota", "Corolla", 2022, 120);
            var customer = new Customer("John", "Doe", "john@doe.com", "34345252", "1234245235");
            var pickUpLocation = new Location("Cundinamarca","Bogotá","Cr 33", "La 33");
            var dropOffLocation = new Location("Antioquia", "Medellín", "Cl 56", "La 56");

            _carRepository.GetByIdAsync(carId).Returns(car);
            _customerRepository.GetByIdAsync(customerId).Returns(customer);
            _locationRepository.GetByIdAsync(pickUpId).Returns(pickUpLocation);
            _locationRepository.GetByIdAsync(dropOffId).Returns(dropOffLocation);

            var command = new CreateReservationCommand
            {
                CarId = carId,
                CustomerId = customerId,
                PickUpLocationId = pickUpId,
                DropOffLocationId = dropOffId,
                Start = DateTime.Today,
                End = DateTime.Today.AddDays(2)
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result);

            await _reservationRepository.Received(1)
                .AddAsync(Arg.Any<Reservation>());

            await _eventDispatcher.Received()
                .DispatchAsync(Arg.Any<CarResevedEvent>());
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenCarDoesNotExist()
        {
            _carRepository.GetByIdAsync(Arg.Any<Guid>())
                .Returns((Car?)null);

            var command = new CreateReservationCommand
            {
                CarId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                PickUpLocationId = Guid.NewGuid(),
                DropOffLocationId = Guid.NewGuid(),
                Start = DateTime.Today,
                End = DateTime.Today.AddDays(1)
            };

            await Assert.ThrowsAsync<DirectoryNotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenCustomerDoesNotExist()
        {
            Location location = new("Cundinamarca", "Bogotá", "Cr 33", "La 33");

            _carRepository.GetByIdAsync(Arg.Any<Guid>())
                .Returns(new Car(location, "ABC123", "Toyota", "Corolla", 2022, 100));

            _customerRepository.GetByIdAsync(Arg.Any<Guid>())
                .Returns((Customer?)null);

            var command = new CreateReservationCommand
            {
                CarId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                PickUpLocationId = Guid.NewGuid(),
                DropOffLocationId = Guid.NewGuid(),
                Start = DateTime.Today,
                End = DateTime.Today.AddDays(1)
            };

            await Assert.ThrowsAsync<DirectoryNotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenPickUpLocationDoesNotExist()
        {
            Location location = new("Cundinamarca", "Bogotá", "Cr 33", "La 33");

            _carRepository.GetByIdAsync(Arg.Any<Guid>())
                .Returns(new Car(location, "ABC123", "Toyota", "Corolla", 2022, 100));

            _customerRepository.GetByIdAsync(Arg.Any<Guid>())
                .Returns(new Customer("John", "Doe", "john@doe.com", "31231234", "23423423"));

            _locationRepository.GetByIdAsync(Arg.Any<Guid>())
                .Returns((Location?)null);

            var command = new CreateReservationCommand
            {
                CarId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                PickUpLocationId = Guid.NewGuid(),
                DropOffLocationId = Guid.NewGuid(),
                Start = DateTime.Today,
                End = DateTime.Today.AddDays(1)
            };

            await Assert.ThrowsAsync<DirectoryNotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
