using Application.Features.SU.User001;
using Application.Models.SU;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Controllers.SU
{
    [Route("api/[controller]")]
    [ApiController]
    public class User001Controller(Lists lists, Detail detail) : ControllerBase
    {
        private readonly Lists _lists = lists;
        private readonly Detail _detail = detail;

        [HttpGet]
        public async Task<ActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var users = await _lists.GetAllUsers(page, pageSize);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            var user = await _detail.GetUserById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            return Ok(null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserDto userDto)
        {
            return Ok(null);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            return Ok(false);
        }
    }
}
