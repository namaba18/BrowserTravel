using Domain.Interfaces.Repositories;
using Domain.Models;
using MediatR;

namespace Aplication.Modules.Car
{
    public class GetCarTypeQuery : IRequest<List<CarType>>
    {        
    }
    public class GetCarTypeQueryHandler : IRequestHandler<GetCarTypeQuery, List<CarType>>
    {
        private readonly ICarTypeRepository _carTypeRepository;
        public GetCarTypeQueryHandler(ICarTypeRepository carTypeRepository)
        {
            _carTypeRepository = carTypeRepository;
        }
        public async Task<List<CarType>> Handle(GetCarTypeQuery request, CancellationToken cancellationToken)
        {
            var carTypes = await _carTypeRepository.GetAsync();
            return await Task.FromResult(carTypes);
        }    
    }
}
