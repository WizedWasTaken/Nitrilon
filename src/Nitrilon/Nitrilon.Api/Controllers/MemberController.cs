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
                Repository repo = new Repository();
                var members = repo.GetAllMembers();

                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Method to get all deleted members from the database.
        /// </summary>
        /// <returns>Method to get all deleted members</returns>
        [HttpGet]
        [Route("deleted")]
        public IActionResult GetDeletedMembers()
        {
            try
            {
                Repository repo = new Repository();
                var members = repo.GetDeletedMembers();

                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Method to get all active members from the database.
        /// </summary>
        /// <returns>List of all active members</returns>
        [HttpGet]
        [Route("active")]
        public IActionResult GetActiveMembers()
        {
            try
            {
                Repository repo = new Repository();
                var members = repo.GetNonDeletedMembers();

                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Method to get a specific member from the database.
        /// </summary>
        [HttpGet]
        [Route("{memberId}")]
        public IActionResult GetMember(int memberId)
        {
            try
            {
                Repository repo = new Repository();
                var member = repo.GetMemberById(memberId);

                return Ok(member);
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
                Repository repo = new Repository();
                int rowId = repo.Create(member);

                return StatusCode(rowId);
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
                Repository repo = new Repository();
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
                Repository repo = new Repository();
                repo.Update(member);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
