using Microsoft.EntityFrameworkCore;

using Postify.Application.Common.Interfaces;

namespace Postify.Infrastructure.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly ApplicationDbContext _context;
        internal DbSet<T> DbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }
        public async Task<T> AddAsync(T item)
        {
            await DbSet.AddAsync(item);
            return item;
        }

        public async virtual Task<List<T>> AllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public bool Delete(T item)
        {
            DbSet.Remove(item);
            return true;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public bool Update(T item)
        {
            DbSet.Update(item);
            return true;
        }
    }
}