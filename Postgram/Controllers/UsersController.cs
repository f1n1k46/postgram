using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postgram.DTOs.User;
using Postgram.Services;
using System.Security.Claims;

namespace Postgram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<IActionResult> GetUserAsync(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound();
            
            return Ok(user);
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            var deleted = await _userService.DeleteUserAsync(userId);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(int userId, [FromBody] UpdateUserDto dto)
        {
            var user = await _userService.UpdateUserAsync(userId, dto);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("{userId}/posts")]
        public async Task<IActionResult> GetUserPostsAsync(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound();

            var posts = await _userService.GetUserPostsAsync(userId);
            return Ok(posts);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Test() {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            return Ok(new
            {
                userId,
                name,
                email
            });
        }
    }
}
 