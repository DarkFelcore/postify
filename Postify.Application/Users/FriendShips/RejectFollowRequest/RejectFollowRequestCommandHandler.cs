using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities.Enums;
using Postify.Domain.Errors;

namespace Postify.Application.Users.FriendShips.RejectFollowRequest
{
    public class RejectFollowRequestCommandHandler : IRequestHandler<RejectFollowRequestCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RejectFollowRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<bool>> Handle(RejectFollowRequestCommand command, CancellationToken cancellationToken)
        {
            var loggedInUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email!);

            if(loggedInUser is null) return Errors.User.NotFound;

            var followRequestUser = await _unitOfWork.UserRepository.GetByIdAsync(command.FollowerId);

            if(followRequestUser is null) return Errors.User.NotFound;

            var notificationToDelete = await _unitOfWork.NotificationRepository.CheckNotificationExistsAsync(command.FollowerId, loggedInUser.Id, NotificationType.FollowRequest);

            if(notificationToDelete is null) return Errors.Notifications.NoNotifications;

            // Security Check
            if(notificationToDelete.Type is not NotificationType.FollowRequest) return Errors.Notifications.NotificationNotFollowingRequest;

            _unitOfWork.NotificationRepository.Delete(notificationToDelete);

            var friendshipToReject = await _unitOfWork.UserRepository.GetFriendShipStatusAsync(command.FollowerId, loggedInUser.Id);
            
            if(friendshipToReject is null) return Errors.User.FriendShipNotFound;

            // Security Check
            if(friendshipToReject.Status is not FriendshipStatus.Pending) return Errors.User.FollowRequestNotPending;

            friendshipToReject.UpdateFriendshipStatus(FriendshipStatus.Rejected);

            _unitOfWork.FriendshipRepository.Update(friendshipToReject);

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}