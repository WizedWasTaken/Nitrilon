using Microsoft.AspNetCore.Mvc;
using Nitrilon.DataAccess;
using Nitrilon.Entities;

namespace Nitrilon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRatingController : ControllerBase
    {

        [HttpGet("{eventId}", Name = "GetEventRatings")]
        public ActionResult<IEnumerable<EventRating>> GetEventRatings(int eventId)
        {
            try
            {
                Repository repo = new Repository();
                var ratings = repo.GetRatingsByEvent(eventId);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost(Name = "AddEventRating")]
        public IActionResult AddEventRating(EventRating rating)
        {
            try
            {
                Repository repo = new Repository();
                int res = repo.Create(rating);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
