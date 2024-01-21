using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Errors;

namespace Postify.Application.Notifications.MarkAsRead
{
    public class MarkNotificationsAsReadCommandHandler : IRequestHandler<MarkNotificationsAsReadCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkNotificationsAsReadCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(MarkNotificationsAsReadCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email!);

            if(user is null) return Errors.User.NotFound;

            var userNotifications = await _unitOfWork.NotificationRepository.GetAllUserNotificationsAsync(user.Id);

            if(userNotifications is null || userNotifications.Count == 0) return Errors.Notifications.NoNotifications;

            foreach (var notification in userNotifications)
            {
                // Update 'isRead' property
                notification.UpdateIsReadStatus(true);
                _unitOfWork.NotificationRepository.Update(notification);
            }

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}