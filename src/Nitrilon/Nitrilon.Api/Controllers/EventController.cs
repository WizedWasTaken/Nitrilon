using Microsoft.AspNetCore.Mvc;
using Nitrilon.Api.types;

namespace Nitrilon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private Event _event;

        [HttpGet(Name = "GetAllEvents")]
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
