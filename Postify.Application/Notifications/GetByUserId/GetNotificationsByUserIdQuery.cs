using ErrorOr;

using MediatR;

using Postify.Application.Notifications.Common;

namespace Postify.Application.Notifications.GetByUserId
{
    public record GetNotificationsByUserIdQuery(
        string? Email
    ) : IRequest<ErrorOr<List<NotificationResult>>>;
}