using Microsoft.EntityFrameworkCore;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;

namespace Postify.Infrastructure.Persistance.Repositories
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<Favorite?> CheckFavoriteExistsAsync(Guid userId, Guid postId)
        {
            return await _context.Favorites.FirstOrDefaultAsync(x => x.UserId == userId && x.PostId == postId);
        }
    }
}