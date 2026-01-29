using Aplication.DTOs;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Aplication.Modules.Car
{
    public class GetAvailableCarsQuery : IRequest<List<CarDto>>
    {
        public Guid LocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetAvailableCarsQueryHandler : IRequestHandler<GetAvailableCarsQuery, List<CarDto>>
    {
        private readonly ICarRepository _carRepository;

        public GetAvailableCarsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<CarDto>> Handle(GetAvailableCarsQuery query, CancellationToken ct)
        {
            List<Domain.Entities.Car> cars = await _carRepository.GetAvailableCarsAsync(query.LocationId, query.StartDate, query.EndDate);

            var result = cars.Select(car => new CarDto
            {
                Id = car.Id,
                Plate = car.Plate,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                PricePerDay = car.PricePerDay,
                Type = car.Type.ToString(),
                Fuel = car.Fuel.ToString(),
                Color = car.Color.ToString(),
                Transmission = car.Transmission.ToString(),
                Status = car.Status.ToString()
            }).ToList();

            return result;
        }
    }
}
