using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyUp.DTOs;
using PartyUp.Services.Interfaces;

namespace PartyUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> WriteComment(CommentDTO commentDto)
        {

            await _commentService.WriteComment(commentDto);
            return Ok(new { Message = "Comment posted successfully" });
        }

        [HttpGet("event/{eventId}")]
        [Authorize]
        public async Task<IActionResult> GetCommentsPerEvent(int eventId)
        {
            var comments = await _commentService.GetCommentsPerEvent(eventId);
            return Ok(comments);
        }


        [HttpPut("{commentId}")]
        [Authorize] 
        public async Task<IActionResult> EditComment(CommentResponseDTO requestBody, int commentId)
        {
            await _commentService.EditComment(requestBody, commentId);
            return Ok(new { Message = "Comment updated successfully" });
        }

        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteComment(commentId);
            return Ok(new { Message = "Comment deleted successfully" });
        }
    }
}

