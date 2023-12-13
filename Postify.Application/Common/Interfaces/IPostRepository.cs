using Postify.Domain.Entities;

namespace Postify.Application.Common.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> AllByUserIdAsync(Guid userId);
    }
}