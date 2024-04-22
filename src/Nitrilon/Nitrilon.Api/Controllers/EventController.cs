using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Nitrilon.DataAccess;
using Nitrilon.Entities;

namespace Nitrilon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        /// <summary>
        /// Action method to get all events from the database.
        /// </summary>
        /// <returns>All events</returns>
        /// <returns>500 if an exception occurs.</returns>
        [HttpGet(Name = "GetAllEvents")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<List<Event>>> GetEvents()
        {
            try
            {
                Repository repo = new Repository();
                var events = repo.GetAllEvents();

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Action method to get a specific event from the database.
        /// </summary>
        /// <param name="id">The id of the event to get.</param>
        /// <returns>The event with the specified id.</returns>
        /// <returns>404 if the event is not found.</returns>
        [HttpGet("{id}", Name = "GetEvent")]
        public IActionResult GetEvent(int id)
        {
            try
            {
                Repository repo = new Repository();
                Event result = repo.GetEventById(id);

                // Quick check to see if the event was found.
                // Honestly isn't required, but pretty nice to have 👨
                if (result.Id == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Action method to get all events after a specific date.
        /// </summary>
        /// <param name="date">Date to search after</param>
        /// <returns>Status 200 with found events</returns>
        [HttpGet]
        [Route("GetEventsAfterDate")]
        public IActionResult GetEventsAfterDate(DateTime date)
        {
            try
            {
                Repository repo = new Repository();
                var events = repo.GetEventsAfterDate(date);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Action method to add a new event to the database.
        /// </summary>
        /// <param name="newEvent">new event object</param>
        /// <returns>new event id</returns>
        /// <returns>500 if an exception occurs.</returns>
        [HttpPost(Name = "AddEvent")]
        public IActionResult AddEvent([FromBody] Event newEvent)
        {
            try
            {
                Repository repo = new Repository();
                int result = repo.Save(newEvent);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Action method to update an existing event in the database.
        /// </summary>
        /// <param name="updatedEvent">updated event object</param>
        /// <returns>updated event</returns>
        /// <returns>500 if an exception occurs.</returns>
        [HttpPut("{id}", Name = "UpdateEvent")]
        public IActionResult UpdateEvent([FromBody] Event updatedEvent)
        {
            try
            {
                Repository repo = new Repository();
                Event result = repo.Update(updatedEvent);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        /// <summary>
        /// Action method to delete an event from the database.
        /// </summary>
        /// <param name="id">id of the event to delete</param>
        /// <returns>Rows affected</returns>
        /// <returns>500 if an exception occurs.</returns>
        /// <returns>404 if the event is not found.</returns>
        [HttpDelete("{id}", Name = "DeleteEvent")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                Repository repo = new Repository();
                int result = repo.DeleteEvent(id);

                if (result == 0)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}