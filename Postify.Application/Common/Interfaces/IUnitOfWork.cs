namespace Postify.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        
        // Repositories
    }
}