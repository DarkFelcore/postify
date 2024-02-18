using Microsoft.EntityFrameworkCore;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;

namespace Postify.Infrastructure.Persistance.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<List<Post>> AllAsync()
        {
            return await _context.Posts
                .Include(x => x.Comments)
                .Include(x => x.User)
                .Include(x => x.PostLikes)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
        public async Task<List<Post>> AllByUserIdAsync(Guid userId)
        {
            return await _context.Posts
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
        public override async Task<Post?> GetByIdAsync(Guid id)
        {
            return await _context.Posts
                .Include(x => x.User)
                .Include(x => x.Comments)!
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.CommentLikes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}