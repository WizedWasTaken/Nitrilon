using System.Data;
using Microsoft.AspNetCore.Mvc;
using Nitrilon.Api.types;

namespace Nitrilon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventRatingController : ControllerBase
    {
        private EventRating _eventRating;
        private readonly DataContext _context;

        public EventRatingController(DataContext context)
        {
            _context = context;
        }

        /*
         * Method to get all event ratings from database.
         */
        [HttpGet(Name = "GetAllEventRatings")]
        public async Task<IActionResult> GetEventRatings()
        {
            try
            {
                var eventRatings = await _context.ExecuteQueryAsync("SELECT * FROM EventRatings", reader =>
                {
                    return new EventRating
                    {
                        Id = reader.GetInt32("EventRatingId"),
                        EventId = reader.GetInt32("EventId"),
                        Rating = reader.GetInt32("RatingId")
                    };
                });
                return Ok(eventRatings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /*
         * Method to get specific event ratings from database.
         */
        [HttpGet("{id}", Name = "GetEventRating")]
        public async Task<IActionResult> GetEventRating(int id)
        {
            try
            {
                var eventRatings = await _context.ExecuteQueryAsync(
                    $"SELECT * FROM EventRatings WHERE EventId = {id}",
                    reader =>
                    {
                        return new EventRating
                        {
                            Id = reader.GetInt32("EventRatingId"),
                            EventId = reader.GetInt32("EventId"),
                            Rating = reader.GetInt32("RatingId")
                        };
                    });

                return Ok(eventRatings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /*
         *
         */
        [HttpPost(Name = "AddEventRating")]
        public async Task<IActionResult> AddEventRating([FromBody] EventRating newEventRating)
        {
            try
            {
                newEventRating.Id = -1;
                await _context.ExecuteNonQueryAsync(
                    $"INSERT INTO EventRatings (EventId, RatingId) VALUES ({newEventRating.EventId}, {newEventRating.Rating})");
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
