using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Application.Notifications.Common;
using Postify.Domain.Entities;
using Postify.Domain.Entities.Enums;
using Postify.Domain.Errors;

namespace Postify.Application.Notifications.GetByUserId
{
    public class GetNotificationsByUserIdQueryHandler : IRequestHandler<GetNotificationsByUserIdQuery, ErrorOr<List<NotificationResult>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNotificationsByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<NotificationResult>>> Handle(GetNotificationsByUserIdQuery query, CancellationToken cancellationToken)
        {
            var mappedUserNotifications = new List<NotificationResult>();

            // Get user notifications
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email!);

            if(user is null) return Errors.User.NotFound;

            var userNotifications = await _unitOfWork.NotificationRepository.GetAllUserNotificationsAsync(user.Id);

            foreach (var notification in userNotifications)
            {
                var userNotificationSender = await _unitOfWork.UserRepository.GetByIdAsync(notification.SenderId);

                // Follow Request Notification
                if(notification.Type == NotificationType.FollowRequest)
                {
                    var friendShip = await _unitOfWork.UserRepository.GetFriendShipStatusAsync(notification.SenderId, notification.ReceiverId);

                    var mappedNotification = new NotificationResult(
                        Message: notification.Message,
                        IsRead: notification.IsRead,
                        Type: notification.Type.ToString(),
                        CreatedAt: notification.CreatedAt.ToString(),
                        SenderId: notification.SenderId
                    ) {
                        FollowRequestUsername = userNotificationSender?.UserName,
                        FollowRequestPictureUrl = userNotificationSender?.PictureUrl != null ? Convert.ToBase64String(userNotificationSender?.PictureUrl!) : string.Empty,
                        FollowRequestCurrentFriendShipStatus = friendShip?.Status.ToString()
                    };

                    mappedUserNotifications.Add(mappedNotification);
                }

                // Follow Request Accepted Notification
                if(notification.Type == NotificationType.FollowAccepted)
                {
                    var mappedNotification = new NotificationResult(
                        Message: notification.Message,
                        IsRead: notification.IsRead,
                        Type: notification.Type.ToString(),
                        CreatedAt: notification.CreatedAt.ToString(),
                        SenderId: notification.SenderId
                    ) {
                        FollowRequestAcceptedUsername = userNotificationSender?.UserName,
                        FollowRequestAcceptedPictureUrl = userNotificationSender?.PictureUrl != null ? Convert.ToBase64String(userNotificationSender?.PictureUrl!) : string.Empty
                    };

                    mappedUserNotifications.Add(mappedNotification);
                }
            }

            return mappedUserNotifications;
        }
    }
}