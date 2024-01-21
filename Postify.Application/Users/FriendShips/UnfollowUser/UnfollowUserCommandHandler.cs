using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities.Enums;
using Postify.Domain.Errors;

namespace Postify.Application.Users.FriendShips.UnfollowUser
{
    public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnfollowUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(UnfollowUserCommand command, CancellationToken cancellationToken)
        {
            var loggedInUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email!);

            if (loggedInUser is null) return Errors.User.NotFound;

            var friendShip = await _unitOfWork.UserRepository.GetFriendShipStatusAsync(loggedInUser.Id, command.UserId);

            if (friendShip is null) return Errors.User.FriendShipNotFound;

            // Check if there is a follow request notification available. If so delete that notification
            var notificationFollowRequest = await _unitOfWork.NotificationRepository.CheckNotificationExistsAsync(loggedInUser.Id, command.UserId, NotificationType.FollowRequest);
            var notificationFollowRequestAccepted = await _unitOfWork.NotificationRepository.CheckNotificationExistsAsync(loggedInUser.Id, command.UserId, NotificationType.FollowAccepted);

            // Delete Follower request notification and or Follow accepted notification
            if(notificationFollowRequest is not null) _unitOfWork.NotificationRepository.Delete(notificationFollowRequest);
            if(notificationFollowRequestAccepted is not null) _unitOfWork.NotificationRepository.Delete(notificationFollowRequestAccepted);

            // Delete friendsip
            _unitOfWork.FriendshipRepository.Delete(friendShip);
            
            // Persist changes
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}