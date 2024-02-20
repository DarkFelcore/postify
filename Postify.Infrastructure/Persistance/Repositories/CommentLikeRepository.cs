using Microsoft.EntityFrameworkCore;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;

namespace Postify.Infrastructure.Persistance.Repositories
{
    public class CommentLikeRepository : GenericRepository<CommentLike>, ICommentLikeRepository
    {
        public CommentLikeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CommentLike?> CheckCommentLikeExistsAsync(Guid commentId, Guid userId)
        {
            return await _context.CommentLikes.FirstOrDefaultAsync(x => x.CommentId == commentId && x.UserId == userId);
        }
    }
}