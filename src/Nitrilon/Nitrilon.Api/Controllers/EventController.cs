using Microsoft.AspNetCore.Mvc;
using Nitrilon.Api.types;

namespace Nitrilon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private Event _event;
        private static List<Event> _events = new(); // List of events (remove when database is implemented)


        /**
         * Method to get all events from database.
         */
        [HttpGet(Name = "GetAllEvents")]
        public IActionResult GetEvents()
        {
            // Everything is temporary, so we will simulate getting events from a database.!!!
            try
            {
                return Ok(_events);
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
                _event = _events.Find(e => e.Id == id);
                if (_event == null) return NotFound();

                return Ok(_event);
            }
            catch
            {
                return BadRequest();
            }
        }

        /*
         * Method to add a new event to the database.
         */
        [HttpPost(Name = "AddEvent")]
        public IActionResult AddEvent([FromBody] Event @event)
        {
            // Everything is temporary, so we will simulate getting events from a database.!!!
            try
            {
                // Check if the event already exists
                int eventId = @event.Id;
                if (_events.Find(e => e.Id == eventId) != null)
                {
                    return BadRequest("Dette event findes allerede i systemet.");
                }

                _events.Add(@event);
                return Ok();
            }
            catch
            {
                return BadRequest("Der skete en fejl. Prøv igen senere.");
            }
        }

        /*
         *  Method to delete a specific event from the database.
         */
        [HttpDelete("{id}", Name = "DeleteEvent")]
        public IActionResult DeleteEvent(int id)
        {
            // Everything is temporary, so we will simulate getting events from a database.!!!
            try
            {
                _event = _events.Find(e => e.Id == id);
                if (_event == null) return NotFound();

                _events.Remove(_event);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
