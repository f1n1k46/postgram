using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postgram.Application.DTOs.PostDto;
using Postgram.Application.Interfaces;
using System.Security.Claims;

namespace Postgram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("{postId}", Name = "GetPost")]
        public async Task<IActionResult> GetPostByIdAsync(int postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var createdPost = await _postService.CreatePostAsync(dto, userId);
            return CreatedAtRoute("GetPost", new { postId = createdPost.PostId }, createdPost);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [Authorize]
        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePostAsync(int postId, [FromBody] UpdatePostDto dto)
        {
            var updatedPost = await _postService.UpdatePostAsync(postId, dto);

            if (updatedPost == null)
                return NotFound();

            return Ok(updatedPost);
        }

        [Authorize]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePostAsync(int postId)
        {
            var deleted = await _postService.DeletePostByIdAsync(postId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}