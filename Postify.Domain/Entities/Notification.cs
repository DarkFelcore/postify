using Postify.Domain.Entities.Enums;
using Postify.Domain.Primitives;

namespace Postify.Domain.Entities
{
    public class Notification : Entity
    {
        public string Message { get; private set; } = string.Empty;
        public bool IsRead { get; private set; }
        public NotificationType Type { get; private set; }
        public Guid SenderId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

        // Relations
        public Guid ReceiverId { get; private set; }
        public User User { get; set; } = null!;

        public Notification()
        {

        }
        public Notification(string message, bool isRead, NotificationType type, Guid receiverId, Guid senderId)
        {
            Message = message;
            ReceiverId = receiverId;
            SenderId = senderId;
            IsRead = isRead;
            Type = type;
        }

        public void UpdateCreationTimeNotification()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateIsReadStatus(bool status)
        {
            IsRead = status;
        }

        public void UpdateNotificationType(NotificationType type)
        {
            Type = type;
        }

        public void UpdateNotificationMessage(string message)
        {
            Message = message;
        }
    }
}