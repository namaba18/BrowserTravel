using Aplication.Features.SearchCars;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;
        private readonly SearchCarsQueryHandler _handler;

        public CarsController(ILogger<CarsController> logger, SearchCarsQueryHandler handler)
        {
            _logger = logger;
            _handler = handler;
        }

        [HttpGet(Name = "GetAvailableCars")]
        public async Task<IActionResult> SearchCars([FromQuery] SearchCarsQuery request, CancellationToken ct)
        {
            var query = new SearchCarsQuery
            {
                LocationId = request.LocationId,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            var result = await _handler.Handle(query, ct);
            return Ok(result);
        }
    }
}
