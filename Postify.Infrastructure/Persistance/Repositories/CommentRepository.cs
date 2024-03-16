using Microsoft.EntityFrameworkCore;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;

namespace Postify.Infrastructure.Persistance.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckParentCommentIdExists(string parentCommentId)
        {
            return await _context.Comments.AnyAsync(x => x.Id == Guid.Parse(parentCommentId));
        }

        public async Task<List<Comment>> GetAllChildComments(string parentCommentId)
        {
            return await _context.Comments.Where(x => x.ParentCommentId == parentCommentId).ToListAsync();
        }
    }
}