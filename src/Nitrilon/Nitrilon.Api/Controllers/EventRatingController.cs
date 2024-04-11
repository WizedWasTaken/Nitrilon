using System.Data;
using Microsoft.AspNetCore.Mvc;
using Nitrilon.Entities;

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
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
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
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
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
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
