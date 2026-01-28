using Aplication.Modules.Car;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using NSubstitute;

namespace UnitTests.Application.Modules.CarTests
{
    public class SearchCarsQueryHandlerTests
    {
        private readonly ICarRepository _carRepository;
        private readonly SearchCarsQueryHandler _handler;

        public SearchCarsQueryHandlerTests()
        {
            _carRepository = Substitute.For<ICarRepository>();
            _handler = new SearchCarsQueryHandler(_carRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedCars_WhenCarsAreAvailable()
        {
            var locationId = Guid.NewGuid();
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(3);

            List<Car> cars =
            [
            new("ABC123", "Toyota", "Corolla", 2022, 120000)
            {
                Id = Guid.NewGuid(),               
                Type = CarType.Sedan,
                Fuel = FuelType.Gasoline,
                Color = Color.White,
                Transmission = TransmissionType.Automatic,
                Status = CarStatus.Available
            }
            ];

            _carRepository
                .GetAvailableAsync(locationId, startDate, endDate)
                .Returns(cars);

            var query = new SearchCarsQuery
            {
                LocationId = locationId,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Count > 0);


            var car = result.First();
            Assert.Equal("ABC123", car.Plate);
            Assert.Equal("Toyota", car.Brand);
            Assert.Equal("Sedan", car.Type);

            await _carRepository.Received(1)
                .GetAvailableAsync(locationId, startDate, endDate);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoCarsAreAvailable()
        {
            _carRepository
                .GetAvailableAsync(Arg.Any<Guid>(), Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns([]);

            var query = new SearchCarsQuery
            {
                LocationId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
