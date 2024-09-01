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
        public async Task<ActionResult<CommentDTO>> WriteComment(CommentDTO commentDto)
        {

            await _commentService.WriteComment(commentDto);
            return Ok(commentDto);
        }

        [HttpGet("event/{eventId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CommentResponseDTO>>> GetCommentsPerEvent(int eventId)
        {
            var comments = await _commentService.GetCommentsPerEvent(eventId);
            return Ok(comments);
        }


        [HttpPut("{commentId}")]
        [Authorize] 
        public async Task<ActionResult> EditComment(CommentResponseDTO requestBody, int commentId)
        {
            await _commentService.EditComment(requestBody, commentId);
            return Ok(new { Message = "Comment updated successfully" });
        }

        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteComment(commentId);
            return Ok(new { Message = "Comment deleted successfully" });
        }
    }
}

