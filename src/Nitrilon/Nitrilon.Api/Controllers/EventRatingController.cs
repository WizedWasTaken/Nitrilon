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
                var ratings = repo.GetRatings(eventId);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }

    //[HttpPost("{eventId}, {ratingId}", Name = "RateEvent")]
    //public IActionResult RateEvent([FromBody] int eventId, [FromBody] int rating)
    //{
    //    try
    //    {
    //        Repository repo = new Repository();
    //        repo.RateEvent(eventId, rating);
    //        return Ok();
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, ex);
    //    }
    //}
}
}
