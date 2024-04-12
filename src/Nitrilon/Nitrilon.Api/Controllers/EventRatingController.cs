using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Nitrilon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRatingController : ControllerBase
    {
        [HttpPost]
        [Route("rate")]
        public IActionResult RateEvent([FromBody] int eventId, [FromBody] int rating)
        {

            return Ok();
        }
    }
}
