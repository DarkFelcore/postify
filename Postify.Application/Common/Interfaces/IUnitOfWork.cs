namespace Postify.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        
        // Repositories
        IUserRepository UserRepository { get; }
        IPostRepository PostRepository { get; }
        IFriendshipRepository FriendshipRepository { get; }
        INotificationRepository NotificationRepository { get; }
    }
}