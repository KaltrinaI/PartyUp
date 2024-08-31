using Microsoft.EntityFrameworkCore;
using PartyUp.Data;
using PartyUp.DTOs;
using PartyUp.Models;
using PartyUp.Repositories.Interfaces;
using System.ComponentModel.Design;
using System.Windows.Input;

namespace PartyUp.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found.");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task EditComment(CommentResponseDTO requestBody, int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found.");
            }

            comment.Content = requestBody.Content;
            comment.CreatedAt = DateTime.UtcNow;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsPerEvent(int eventId)
        {
                return await _context.Comments
            .Where(c => c.EventId == eventId)
            .Include(c => c.User)
            .ToListAsync();
        }

        public async Task WriteComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            comment.CreatedAt = DateTime.UtcNow;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}
