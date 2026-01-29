using Aplication.Modules.Car;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models;
using NSubstitute;

namespace UnitTests.Application.Modules.CarTests
{
    public class SearchCarsQueryHandlerTests
    {
        private readonly ISearchCarRepository _mongoRepository;
        private readonly ICarRepository _carRepository;
        private readonly GetAvailableCarsQueryHandler _handler;

        public SearchCarsQueryHandlerTests()
        {
            _carRepository = Substitute.For<ICarRepository>();
            _mongoRepository = Substitute.For<ISearchCarRepository>();
            _handler = new GetAvailableCarsQueryHandler(_mongoRepository, _carRepository);
        }

        [Fact]
        public async Task Handle_WhenCacheExists_ReturnsCarsFromMongo()
        {
            // Arrange
            var query = new GetAvailableCarsQuery
            {
                LocationId = Guid.NewGuid(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(3)
            };

            var cached = new SearchCar
            {
                Cars =
            {
                new CarResult
                {
                    CarId = Guid.NewGuid().ToString(),
                    Model = "Mazda 3",
                    PricePerDay = 120
                }
            }
            };

            _mongoRepository
                .GetAsync(query.LocationId, query.StartDate, query.EndDate, Arg.Any<CancellationToken>())
                .Returns(cached);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Mazda 3", result[0].Model);
            Assert.Equal(120, result[0].PricePerDay);

            // ❗ Validación CLAVE
            await _carRepository
                .DidNotReceive()
                .GetAvailableCarsAsync(Arg.Any<Guid>(), Arg.Any<DateTime>(), Arg.Any<DateTime>());
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedCars_WhenCarsAreAvailable()
        {
            Location location = new("Cundinamarca", "Bogotá", "Cl 7", "La 7");
            var locationId = location.Id;
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(3);

            List<Car> cars =
            [
            new(location, "ABC123", "Toyota", "Corolla", 2022, 120000)
            {
                Id = Guid.NewGuid(),               
                Type = CarType.Sedan,
                Fuel = FuelType.Gasoline,
                Color = Color.White,
                Transmission = TransmissionType.Automatic,
                Status = CarStatus.Available
            }
            ];

            _mongoRepository
                .GetAsync(locationId, startDate, endDate, new CancellationToken())
                .Returns((SearchCar?)null);

            _carRepository
                .GetAvailableCarsAsync(locationId, startDate, endDate)
                .Returns(cars);

            var query = new GetAvailableCarsQuery
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
                .GetAvailableCarsAsync(locationId, startDate, endDate);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoCarsAreAvailable()
        {
            _carRepository
                .GetAvailableCarsAsync(Arg.Any<Guid>(), Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns([]);

            var query = new GetAvailableCarsQuery
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
