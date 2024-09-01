using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Nitrilon.DataAccess;
using Nitrilon.Entities;

namespace Nitrilon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HejController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<List<Event>>> GetEvents()
        {
            return Ok("Hej");
        }
    }
}