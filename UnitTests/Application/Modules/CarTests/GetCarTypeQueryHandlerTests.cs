using Aplication.Modules.Car;
using Domain.Interfaces.Repositories;
using Domain.Models;
using NSubstitute;

namespace UnitTests.Application.Modules.CarTests
{
    public class GetCarTypeQueryHandlerTests
    {
        private readonly ICarTypeRepository _carTypeRepository;
        private readonly GetCarTypeQueryHandler _handler;

        public GetCarTypeQueryHandlerTests()
        {
            _carTypeRepository = Substitute.For<ICarTypeRepository>();
            _handler = new GetCarTypeQueryHandler(_carTypeRepository);
        }

        [Fact]
        public async Task Handle_Should_Return_List_Of_CarTypes()
        {
            var carTypes = new List<CarType>
            {
                new CarType { Id = Guid.NewGuid().ToString(), Name = "SUV" },
                new CarType { Id = Guid.NewGuid().ToString(), Name = "Sedan" }
            };

            _carTypeRepository.GetAsync()
                .Returns(carTypes);

            var query = new GetCarTypeQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("SUV", result[0].Name);

            await _carTypeRepository.Received(1).GetAsync();
        }

        [Fact]
        public async Task Handle_Should_Return_Empty_List_When_No_CarTypes_Exist()
        {
            _carTypeRepository.GetAsync()
                .Returns(new List<CarType>());

            var query = new GetCarTypeQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Empty(result);

            await _carTypeRepository.Received(1).GetAsync();
        }
    }
}

