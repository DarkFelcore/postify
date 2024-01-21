using ErrorOr;

using MediatR;

namespace Postify.Application.Notifications.MarkAsRead
{
    public record MarkNotificationsAsReadCommand(
        string? Email
    ) : IRequest<ErrorOr<bool>>;
}