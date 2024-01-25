using Postify.Domain.Entities;

namespace Postify.Application.Common.Interfaces
{
    public interface IPostLikeRepository : IGenericRepository<PostLike>
    {
        Task<PostLike?> CheckPostLikeExistsAsync(Guid postId, Guid userId);
    }
}