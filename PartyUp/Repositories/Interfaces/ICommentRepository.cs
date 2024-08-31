using PartyUp.DTOs;
using PartyUp.Models;

namespace PartyUp.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsPerEvent(int eventId);
        Task WriteComment(Comment comment);
        Task EditComment(CommentResponseDTO requestBody, int commentId);
        Task DeleteComment(int commentId);
    }
}
