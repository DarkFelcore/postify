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

            var friendShip = await _unitOfWork.UserRepository.GetFriendShipStatusAsync(loggedInUser.Id, command.UserId);

            if(friendShip is not null)
            {
                friendShip.UpdateFriendshipStatus(FriendshipStatus.Pending);
                _unitOfWork.FriendshipRepository.Update(friendShip);
            }
            else
            {
                friendShip = new Follower(loggedInUser.Id, command.UserId, FriendshipStatus.Pending);
                await _unitOfWork.FriendshipRepository.AddAsync(friendShip);
            }

            var followRequestUser = await _unitOfWork.UserRepository.GetByIdAsync(friendShip.FollowerId);

            if (followRequestUser is null) return Errors.User.NotFound;

            var newNotification = new Notification(
                message: $"{followRequestUser.UserName} wants to follow you.",
                isRead: false,
                type: NotificationType.FollowRequest,
                receiverId: friendShip.FollowedId,
                senderId: friendShip.FollowerId
            );

            var existingNotification = await _unitOfWork.NotificationRepository.CheckNotificationExistsAsync(friendShip.FollowerId, friendShip.FollowedId, NotificationType.FollowRequest);

            // Replace existingNotification with notification
            if (existingNotification is not null)
            {
                // Only update the creation time to the current timestamp
                existingNotification.UpdateCreationTimeNotification();
                _unitOfWork.NotificationRepository.Update(existingNotification);
            }
            // Add new notification
            else
            {
                await _unitOfWork.NotificationRepository.AddAsync(newNotification);
            }

            // Save changes to the database
            await _unitOfWork.CompleteAsync();

            var followedUserConnectionId = _userConnectionService.GetUserConnectionId(friendShip.FollowedId.ToString());

            // Send notification to the the notification hub
            if (followedUserConnectionId is not null) await _notification.Clients.Client(followedUserConnectionId!).SendNotification(newNotification);

            return true;
        }
    }
}