using Postify.Domain.Entities;

namespace Postify.Application.Common.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetPostLikeUsers(List<PostLike> postLikes);
    }
}