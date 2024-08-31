using PartyUp.DTOs;
using PartyUp.Models;

namespace PartyUp.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponseDTO>> GetCommentsPerEvent(int eventId);
        Task WriteComment(CommentDTO commentDto);
        Task EditComment(CommentResponseDTO requestBody, int commentId);
        Task DeleteComment(int commentId);
    }
}
