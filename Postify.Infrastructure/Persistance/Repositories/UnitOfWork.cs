using Postify.Application.Common.Interfaces;

namespace Postify.Infrastructure.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository UserRepository { get; private set; }
        public IPostRepository PostRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            // Repositories
            UserRepository = new UserRepository(context);
            PostRepository = new PostRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}