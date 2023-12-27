using Microsoft.EntityFrameworkCore;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;
using Postify.Domain.Entities.Enums;

namespace Postify.Infrastructure.Persistance.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Notification?> CheckFollowRequestNotificationExistsAsync(Notification notification)
        {
            return await _context.Notifications.FirstOrDefaultAsync(x =>
                x.Type == NotificationType.FollowRequest &&
                x.SenderId == notification.SenderId &&
                x.ReceiverId == notification.ReceiverId);
        }
    }
}