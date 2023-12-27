using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.SignalR;

using Postify.Application.Common.Hubs;
using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;
using Postify.Domain.Entities.Enums;
using Postify.Domain.Errors;

namespace Postify.Application.Users.FriendShips.FollowUser
{
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserConnectionService _userConnectionService;
        private readonly IHubContext<NotificationHub, INotificationHub> _notification;

        public FollowUserCommandHandler(IUnitOfWork unitOfWork, IHubContext<NotificationHub, INotificationHub> hubContext, IUserConnectionService userConnectionService)
        {
            _unitOfWork = unitOfWork;
            _notification = hubContext;
            _userConnectionService = userConnectionService;
        }

        public async Task<ErrorOr<bool>> Handle(FollowUserCommand command, CancellationToken cancellationToken)
        {
            var loggedInUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email!);

            if (loggedInUser is null) return Errors.User.NotFound;

            var newFriendShip = new Follower(loggedInUser.Id, command.UserId, FriendshipStatus.Pending);

            await _unitOfWork.FriendshipRepository.AddAsync(newFriendShip);

            var followRequestUser = await _unitOfWork.UserRepository.GetByIdAsync(newFriendShip.FollowerId);

            if (followRequestUser is null) return Errors.User.NotFound;

            var notification = new Notification(
                message: $"{followRequestUser.UserName} wants to follow you.",
                isRead: false,
                type: NotificationType.FollowRequest,
                receiverId: newFriendShip.FollowedId,
                senderId: newFriendShip.FollowerId
            );

            var existingNotification = await _unitOfWork.NotificationRepository.CheckFollowRequestNotificationExistsAsync(notification);

            // Replace existingNotification with notification
            if (existingNotification is not null)
            {
                existingNotification.UpdateNotification();
                _unitOfWork.NotificationRepository.Update(existingNotification);
            }
            // Add new notification
            else
            {
                await _unitOfWork.NotificationRepository.AddAsync(notification);
            }

            // Save changes to the database
            await _unitOfWork.CompleteAsync();

            var followedUserConnectionId = _userConnectionService.GetUserConnectionId(newFriendShip.FollowedId.ToString());

            // Send notification to the the notification hub
            if (followedUserConnectionId is not null) await _notification.Clients.Client(followedUserConnectionId!).SendNotification(notification);

            return true;
        }
    }
}