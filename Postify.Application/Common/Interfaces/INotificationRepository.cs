using Postify.Domain.Entities;
using Postify.Domain.Entities.Enums;

namespace Postify.Application.Common.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<Notification?> CheckNotificationExistsAsync(Guid senderId, Guid receiverId, NotificationType type);
        Task<List<Notification>> GetAllUserNotificationsAsync(Guid userId);
    }
}