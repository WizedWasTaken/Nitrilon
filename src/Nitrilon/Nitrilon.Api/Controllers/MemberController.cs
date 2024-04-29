using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nitrilon.DataAccess;
using Nitrilon.Entities;

namespace Nitrilon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetMembers()
        {
            try
            {
                Repository repo = new Repository();
                var members = repo.GetAllMembers();

                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult AddMember([FromBody] Member member)
        {
            try
            {
                Repository repo = new Repository();
                repo.Create(member);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
