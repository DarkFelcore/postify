using Postify.Domain.Entities;

namespace Postify.Application.Common.Interfaces
{
    public interface INotificationHub
    {
        Task SendNotification(Notification notification);
    }
}