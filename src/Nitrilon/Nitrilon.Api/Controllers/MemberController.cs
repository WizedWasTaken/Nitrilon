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
        /// <summary>
        /// IActionResult method to get all members from the database.
        /// </summary>
        /// <returns>All members</returns>
        [HttpGet]
        [Route("all")]
        public IActionResult GetAllMembers()
        {
            try
            {
                MemberRepository repo = new();
                var members = repo.GetAllMembers();

                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// IActionResult method to create a new member in the database.
        /// </summary>
        /// <param name="member">Member object</param>
        /// <returns>new row id</returns>
        [HttpPost]
        public IActionResult AddMember([FromBody] Member member)
        {
            try
            {
                MemberRepository repo = new();
                repo.CreateMember(member);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Method to update an existing member in the database.
        /// </summary>
        /// <param name="memberId">MemberId to soft delete.</param>
        /// <returns>201 or 500</returns>
        [HttpPut]
        public IActionResult SoftDelete(int memberId)
        {
            try
            {
                MemberRepository repo = new();
                repo.SoftDelete(memberId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Method to update an existing member in the database.
        /// </summary>
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateMember([FromBody] Member member)
        {
            try
            {
                MemberRepository repo = new();
                repo.UpdateMember(member);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
