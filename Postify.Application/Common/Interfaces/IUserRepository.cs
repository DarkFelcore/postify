using Postify.Domain.Entities;

namespace Postify.Application.Common.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetPostLikeUsers(List<PostLike> postLikes);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<Follower?> GetFriendShipStatusAsync(Guid userId, Guid profileId);
        Task<List<Follower>> GetUserFollowersAsync(Guid userId);
        Task<List<Follower>> GetUserFollowingsAsync(Guid userId);
    }
}