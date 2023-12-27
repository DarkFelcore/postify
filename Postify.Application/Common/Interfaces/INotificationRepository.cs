using Postify.Domain.Entities;

namespace Postify.Application.Common.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<Notification?> CheckFollowRequestNotificationExistsAsync(Notification notification);
    }
}