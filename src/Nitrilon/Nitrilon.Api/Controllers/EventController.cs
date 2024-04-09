using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nitrilon.Api.schemas;
using Nitrilon.Api.types;
using Swashbuckle.AspNetCore.Filters;

namespace Nitrilon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private Event _event;

        [HttpGet(Name = "GetAllEvents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EventExample))]
        public IActionResult GetEvents()
        {
            List<Event> events = new List<Event>();
            try
            {
                // Simulate getting events from a database
                for (int i = 1; i <= 5; i++)
                {
                    _event = new Event
                    {
                        Id = i,
                        Name = $"Event {i}",
                        Date = $"2021-01-0{i}",
                        Attendees = $"John Doe, Jane Doe",
                        Description = $"This is event {i}"
                    };
                    events.Add(_event);
                }

                return Ok(events);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
