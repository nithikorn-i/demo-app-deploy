using Application.Features.SU.Win001;
using Application.Models.SU;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Controllers.SU
{
    [Route("api/[controller]")]
    [ApiController]
    public class Win001Controller(Lists lists) : ControllerBase
    {
        private readonly Lists _lists = lists;

        [HttpGet("getAllUser")]
        public async Task<ActionResult> GetAllUsers([FromQuery] int page, [FromQuery] int pageSize)
        {
            var users = await _lists.GetAllUsers(page, pageSize);
            return Ok(users);
        }

        [HttpGet("getUser")]
        public async Task<ActionResult> GetUserById([FromQuery] Guid id)
        {
            var win = await _lists.GetUserById(id);
            return Ok(win);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] WinDto win)
        {
            var data = await _lists.CreateUser(win);
            return Ok(data);
        }
        
        [HttpPut("Update")]
        public async Task<ActionResult> UpdateUser([FromQuery] Guid id,[FromQuery] String fullname,[FromQuery] string email )
        {
            var data = await _lists.UpdateUser(id, fullname, email);
            return Ok(data);
        }
        
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteUser([FromQuery] Guid id)
        {
            var data = await _lists.DeleteUser(id);
            return Ok(data);
        }
    }
}
