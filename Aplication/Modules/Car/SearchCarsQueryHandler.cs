using Aplication.DTOs;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Aplication.Modules.Car
{
    public class SearchCarsQuery : IRequest<List<SearchCarsResponse>>
    {
        public Guid LocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class SearchCarsQueryHandler : IRequestHandler<SearchCarsQuery, List<SearchCarsResponse>>
    {
        private readonly ICarRepository _carRepository;

        public SearchCarsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<SearchCarsResponse>> Handle(SearchCarsQuery request, CancellationToken ct)
        {
            List<Domain.Entities.Car> cars = await _carRepository.GetAvailableAsync(request.LocationId, request.StartDate, request.EndDate);

            var response = cars.Select(car => new SearchCarsResponse
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

            return response;
        }
    }
}
