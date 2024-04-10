using Microsoft.AspNetCore.Mvc;
using Nitrilon.Api.types;
using System.Data;
using System.Diagnostics;

namespace Nitrilon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private Event _event;

        private readonly DataContext _context;
        public EventController(DataContext context)
        {
            _context = context;
        }

        /*
         * Method to get all events from database.
         */
        [HttpGet(Name = "GetAllEvents")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = await _context.ExecuteQueryAsync("SELECT * FROM Events", reader =>
                {
                    return new Event
                    {
                        Id = reader.GetInt32("EventId"), // Using column names is safer than column indices
                        Name = reader.GetString("Name"),
                        Date = reader.GetDateTime("Date"),
                        Attendees = reader.GetInt32("Attendees"),
                        Description = reader.GetString("Description")
                    };
                });
                return Ok(events);
            }
            catch (Exception ex)
            {
                // Log the exception details (ex) here
                // For simplicity, just printing to console
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return a 500 Internal Server Error or another appropriate status code
                return StatusCode(500, "An error occurred while processing your request.");
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
                var events = await _context.ExecuteQueryAsync($"SELECT * FROM Events WHERE EventId = {id}", reader =>
                {
                    return new Event
                    {
                        Id = reader.GetInt32("EventId"),
                        Name = reader.GetString("Name"),
                        Date = reader.GetDateTime("Date"),
                        Attendees = reader.GetInt32("Attendees"),
                        Description = reader.GetString("Description")
                    };
                });

                if (events.Count == 0)
                {
                    return NotFound();
                }

                return Ok(events[0]);
            }
            catch
            {
                return BadRequest();
            }
        }

        /*
         *  Method to add a new event to the database.
         */
        [HttpPost(Name = "AddEvent")]
        public async Task<IActionResult> AddEvent([FromBody] Event newEvent)
        {
            // Everything is temporary, so we will simulate getting events from a database.!!!
            try
            {
                newEvent.Id = -1; // Reset possible input'd ID
                await _context.ExecuteNonQueryAsync($"INSERT INTO Events (Name, Date, Attendees, Description) VALUES ('{newEvent.Name}', '{newEvent.Date}', {newEvent.Attendees}, '{newEvent.Description}')");
                return Ok();
            }
            catch
            {
                return BadRequest();
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
                await _context.ExecuteNonQueryAsync($"UPDATE Events SET Name = '{updatedEvent.Name}', Date = '{updatedEvent.Date}', Attendees = {updatedEvent.Attendees}, Description = '{updatedEvent.Description}' WHERE EventId = {id}");
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


        /*
         *  Method to delete a specific event from the database.
         */
        [HttpDelete("{id}", Name = "DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                string sql = $"DELETE FROM EventRatings WHERE EventId = {id}; DELETE FROM Events WHERE EventId = {id}";
                int result = await _context.ExecuteNonQueryAsync(sql);

                Debug.WriteLine($"Delete SQL Result: {result}");
                if (result is -1 or 0)
                {
                    return NotFound("Eventet blev ikke fundet.");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}