using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreateReservationController : ControllerBase
    {        
        private readonly ILogger<CreateReservationController> _logger;

        public CreateReservationController(ILogger<CreateReservationController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "CreateReservation")]
        public string Posto()
        {
            return "Reservation success";
        }
    }
}
