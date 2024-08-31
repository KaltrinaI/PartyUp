using AutoMapper;
using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Repositories.Interfaces;
using PartyUp.Services.Interfaces;

namespace PartyUp.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService (ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task DeleteComment(int commentId)
        {
            await _commentRepository.DeleteComment(commentId);
        }

        public async Task EditComment(CommentResponseDTO requestBody, int commentId)
        {
            await _commentRepository.EditComment(requestBody, commentId);
        }

        public async Task<IEnumerable<CommentResponseDTO>> GetCommentsPerEvent(int eventId)
        {
            var comments = await _commentRepository.GetCommentsPerEvent(eventId);
            return _mapper.Map<IEnumerable<CommentResponseDTO>>(comments);
        }

        public async Task WriteComment(CommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            comment.CreatedAt = DateTime.UtcNow;

            await _commentRepository.WriteComment(comment);
        }
    }
}
