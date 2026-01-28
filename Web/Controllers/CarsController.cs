using Aplication.Features.SearchCars;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;
        private readonly ISender _sender;

        public CarsController(ILogger<CarsController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpGet(Name = "GetAvailableCars")]
        public async Task<IActionResult> SearchCars([FromQuery] SearchCarsQuery query, CancellationToken ct)
        {
            var result = await _sender.Send(query, ct);
            return Ok(result);
        }
    }
}
