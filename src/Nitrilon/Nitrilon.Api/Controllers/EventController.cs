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
        /*
         * Method to get all events from database.
         */
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


        /*
        * Method to get a specific event from database.
        */
        [HttpGet("{id}", Name = "GetEvent")]
        public async Task<IActionResult> GetEvent(int id)
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
         *  Method to add a new event to the database.
         */
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

        /*
         *  Method to update a specific event in the database.
         */
        [HttpPut("{id}", Name = "UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            // Everything is temporary, so we will simulate getting events from a database.!!!
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
         *  Method to delete a specific event from the database.
         */
        [HttpDelete("{id}", Name = "DeleteEvent")]
        public async Task<IActionResult> DeleteEvent()
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