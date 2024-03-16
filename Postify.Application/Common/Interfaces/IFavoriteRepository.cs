using Postify.Domain.Entities;

namespace Postify.Application.Common.Interfaces
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<Favorite?> CheckFavoriteExistsAsync(Guid userId, Guid postId);
    }
}