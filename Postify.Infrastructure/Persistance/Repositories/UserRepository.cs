using Microsoft.EntityFrameworkCore;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;

namespace Postify.Infrastructure.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetPostLikeUsers(List<PostLike> postLikes)
        {
            var users = new List<User>();

            for (int i = 0; i < postLikes.Count; i++)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == postLikes[i].UserId);

                users.Add(user!);
            }

            return users;
        }
    }
}