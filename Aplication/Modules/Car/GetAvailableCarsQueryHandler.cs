using Aplication.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Models;
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
        private readonly ISearchCarRepository _mongoRepo;
        private readonly ICarRepository _carRepository;

        public GetAvailableCarsQueryHandler(ISearchCarRepository mongoRepo, ICarRepository carRepository)
        {
            _mongoRepo = mongoRepo;
            _carRepository = carRepository;
        }

        public async Task<List<CarDto>> Handle(GetAvailableCarsQuery query, CancellationToken ct)
        {
            var cached = await _mongoRepo.GetAsync(query.LocationId, query.StartDate, query.EndDate, ct);

            if (cached != null && cached.Cars.Count > 0)
                return cached.Cars.Select(car => new CarDto
                {
                    Id = Guid.Parse(car.CarId),
                    Plate = car.Plate,
                    Status = car.Status,
                    Model = car.Model,
                    Brand = car.Brand,
                    Color = car.Color,
                    Year = car.Year,
                    Fuel = car.Fuel,
                    Transmission = car.Transmission,
                    Type = car.Type,
                    PricePerDay = car.PricePerDay
                }).ToList();

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

            await _mongoRepo.SaveAsync(new SearchCar
            {
                Id = Guid.NewGuid().ToString(),
                LocationId = query.LocationId.ToString(),
                Start = query.StartDate,
                End = query.EndDate,
                Cars = result.Select(c => new CarResult
                {
                    CarId = c.Id.ToString(),
                    Model = c.Model,
                    Plate = c.Plate,
                    Brand = c.Brand,
                    Year = c.Year,
                    Type = c.Type,
                    Fuel = c.Fuel.ToString(),
                    Color = c.Color.ToString(),
                    Transmission = c.Transmission.ToString(),
                    Status = c.Status.ToString(),
                    PricePerDay = c.PricePerDay
                }).ToList()
            });

            return result;
        }
    }
}
