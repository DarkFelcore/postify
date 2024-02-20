using Postify.Domain.Entities;

namespace Postify.Application.Common.Interfaces
{
    public interface ICommentLikeRepository : IGenericRepository<CommentLike>
    {
        Task<CommentLike?> CheckCommentLikeExistsAsync(Guid commentId, Guid userId);
    }
}