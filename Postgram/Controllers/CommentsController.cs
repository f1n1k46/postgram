using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postgram.DTOs.CommentDto;
using Postgram.Services;
using System.Security.Claims;

namespace Postgram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [AllowAnonymous]
        [HttpGet("{commentId}", Name = "GetComment")]
        public async Task<IActionResult> GetCommentByIdAsync(int commentId)
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);

            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var createdComment = await _commentService.CreateCommentAsync(dto, userId);
            return CreatedAtRoute("GetComment", new { commentId = createdComment.CommentId }, createdComment);
        }

        [Authorize]
        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateCommentAsync(int commentId, [FromBody] UpdateCommentDto dto)
        {
            var updatedComment = await _commentService.UpdateCommentAsync(commentId, dto);

            if (updatedComment == null)
                return NotFound();

            return Ok(updatedComment);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteCommentAsync(int commentId)
        {
            var deleted = await _commentService.DeleteCommentByIdAsync(commentId);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCommentsAsync()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return Ok(comments);
        }
    }
}
