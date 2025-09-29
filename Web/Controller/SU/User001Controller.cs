using Application.Features.SU.User001;
using Application.Models.SU;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller.SU
{
    [Route("api/[controller]")]
    [ApiController]
    public class User001Controller(Lists lists) : ControllerBase
    {
        private readonly Lists _lists = lists;

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var users = await _lists.GetAllUsers(page, pageSize);
            return Ok(users);
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult> GetUserById([FromQuery] Guid id)
        {
            var users = await _lists.GetUserById(id);
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var users = await _lists.CreateUser(userDto);
            return Ok(users);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            var users = await _lists.UpdateUser(userDto);
            return Ok(users);
        }
        
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser([FromQuery] Guid id)
        {
            var users = await _lists.DeleteUser(id);
            return Ok(users);
        }
    }
}
