using System.Collections.Generic;
using System.Threading.Tasks;
using PartyUp.Models;

namespace PartyUp.Services
{
    public interface ICommentsService
    {
        Task<Comment> PostComment(Comment comment);
        Task<IEnumerable<Comment>> GetCommentsPerEvent(int eventId);
    }
}