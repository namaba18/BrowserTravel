using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : ControllerBase
    {        
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(ILogger<ReservationsController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "CreateReservation")]
        public string Post()
        {
            return "Reservation success";
        }
    }
}
