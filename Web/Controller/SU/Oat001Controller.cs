using Application.Features.SU.Oat001;
using Application.Models.SU;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller.SU
{
    [Route("api/[controller]")]
    [ApiController]
    public class Oat001Controller(ListOat lists) : ControllerBase
    {
        private readonly ListOat _lists = lists;

        [HttpGet("GetAllOats")]
        public async Task<ActionResult> GetAllOats([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var Oats = await _lists.GetAllOats(page, pageSize);
            return Ok(Oats);
        }

        [HttpGet("GetOatById")]
        public async Task<ActionResult> GetOatById([FromQuery] Guid id)
        {
            var Oats = await _lists.GetOatById(id);
            return Ok(Oats);
        }

        [HttpPost("CreateOat")]
        public async Task<ActionResult> CreateOat([FromBody] OatDto OatDto)
        {
            var Oats = await _lists.CreateOat(OatDto);
            return Ok(Oats);
        }

        [HttpPut("UpdateOat")]
        public async Task<ActionResult> UpdateOat([FromBody] OatDto OatDto)
        {
            var Oats = await _lists.UpdateOat(OatDto);
            return Ok(Oats);
        }
        
        [HttpDelete("DeleteOat")]
        public async Task<ActionResult> DeleteOat([FromQuery] Guid id)
        {
            var Oats = await _lists.DeleteOat(id);
            return Ok(Oats);
        }
    }
}
