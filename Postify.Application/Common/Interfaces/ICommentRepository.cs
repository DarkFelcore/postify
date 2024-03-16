using Postify.Domain.Entities;

namespace Postify.Application.Common.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<bool> CheckParentCommentIdExists(string parentCommentId);
        Task<List<Comment>> GetAllChildComments(string parentCommentId);
    }
}