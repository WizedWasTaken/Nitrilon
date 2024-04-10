using Microsoft.AspNetCore.Mvc;
using Nitrilon.Api.types;

namespace Nitrilon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private Event _event;
        List<Event> events = new List<Event>();

        /**
         * Method to get all events from database.
         */
        [HttpGet(Name = "GetAllEvents")]
        public IActionResult GetEvents()
        {
            // Everything is temporary, so we will simulate getting events from a database.!!!
            try
            {
                GenerateEvents();
                return Ok(events);
            }
            catch
            {
                return BadRequest();
            }
        }

        /*
        * Method to get a specific event from database.
        */
        [HttpGet("{id}", Name = "GetEvent")]
        public IActionResult GetEvent(int id)
        {
            // Everything is temporary, so we will simulate getting events from a database.!!!
            try
            {
                GenerateEvents();
                _event = events.Find(e => e.Id == id);
                return Ok(_event);
            }
            catch
            {
                return BadRequest();
            }
        }
        
        private void GenerateEvents()
        {
            // Temporary solution to clear the list
            foreach (var _event in events)
            {
                events.Remove(_event);
            }

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
        }
    }
}
