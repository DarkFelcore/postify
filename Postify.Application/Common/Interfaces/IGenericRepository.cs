namespace Postify.Application.Common.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<List<T>> AllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T> AddAsync(T item);
        bool Update(T item);
        bool Delete(T item);
    }
}
