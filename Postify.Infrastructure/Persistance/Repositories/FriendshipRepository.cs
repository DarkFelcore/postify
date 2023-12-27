using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;

namespace Postify.Infrastructure.Persistance.Repositories
{
    public class FriendshipRepository : GenericRepository<Follower>, IFriendshipRepository
    {
        public FriendshipRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}