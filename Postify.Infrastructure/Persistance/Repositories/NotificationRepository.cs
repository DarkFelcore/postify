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

        public async Task<Notification?> CheckNotificationExistsAsync(Guid senderId, Guid receiverId, NotificationType type)
        {
            return await _context.Notifications.FirstOrDefaultAsync(x =>
                x.Type == type &&
                x.SenderId == senderId &&
                x.ReceiverId == receiverId);
        }

        public async Task<List<Notification>> GetAllUserNotificationsAsync(Guid userId)
        {
            return await _context.Notifications.Where(x => x.ReceiverId == userId).ToListAsync();
        }
    }
}