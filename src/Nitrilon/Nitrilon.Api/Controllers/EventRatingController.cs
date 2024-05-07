using Microsoft.AspNetCore.Mvc;
using Nitrilon.DataAccess;
using Nitrilon.Entities;

namespace Nitrilon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRatingController : ControllerBase
    {

        /// <summary>
        /// Method to get ratings for a specific event.
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns>Status 200 with count of the 3 possible ratings.</returns>
        [HttpGet("{eventId}", Name = "GetEventRatings")]
        public ActionResult<IEnumerable<EventRatingData>> GetEventRatings(int eventId)
        {
            try
            {
                EventRepository repo = new();
                var ratings = repo.GetRatingsByEvent(eventId);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Method to add a rating to an event.
        /// </summary>
        /// <param name="EventId">Id of the event</param>
        /// <param name="RatingId">Rating to give</param>
        /// <returns>Status 200 with id of the new event rating</returns>
        [HttpPost(Name = "AddEventRating")]
        // Is [FromQuery] actually needed?
        public IActionResult AddEventRating([FromQuery] int EventId, [FromQuery] int RatingId)
        {
            try
            {
                EventRepository repo = new();
                int res = repo.CreateRating(EventId, RatingId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
