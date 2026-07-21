using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postgram.Application.DTOs.LikeDto;
using Postgram.Application.Interfaces;
using System.Security.Claims;

namespace Postgram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        public async Task<IActionResult> LikePostAsync([FromBody] LikeDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _likeService.LikePostAsync(dto, userId);

            if (!result)
                return BadRequest("Unable to like the post.");

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> UnlikePostAsync([FromBody] LikeDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _likeService.UnlikePostAsync(dto, userId);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
 