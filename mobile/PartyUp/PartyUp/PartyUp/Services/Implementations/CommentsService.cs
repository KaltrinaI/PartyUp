using System.Collections.Generic;
using System.Threading.Tasks;
using PartyUp.Models;

namespace PartyUp.Services
{
    public class CommentsService : ICommentsService
    {
        
        private readonly IHttpService _httpService;

        public CommentsService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public Task<Comment> PostComment(Comment comment)
        {
            return _httpService.PostAsync<Comment>("api/Comment",comment);
        }

        public Task<IEnumerable<Comment>> GetCommentsPerEvent(int eventId)
        {
            return _httpService.GetAsync<IEnumerable<Comment>>($"api/comment/event/{eventId}");
        }
    }
}