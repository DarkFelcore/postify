using Microsoft.EntityFrameworkCore;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;

namespace Postify.Infrastructure.Persistance.Repositories
{
    public class PostLikeRepository : GenericRepository<PostLike>, IPostLikeRepository
    {
        public PostLikeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PostLike?> CheckPostLikeExistsAsync(Guid postId, Guid userId)
        {
            return await _context.PostLikes.FirstOrDefaultAsync(x => x.UserId == userId && x.PostId == postId);
        }
    }
}