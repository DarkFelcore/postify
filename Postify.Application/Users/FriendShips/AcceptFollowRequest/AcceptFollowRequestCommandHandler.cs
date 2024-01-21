using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;
using Postify.Domain.Entities.Enums;
using Postify.Domain.Errors;

namespace Postify.Application.Users.FriendShips.AcceptFollowRequest
{
    public class AcceptFollowRequestCommandHandler : IRequestHandler<AcceptFollowRequestCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AcceptFollowRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(AcceptFollowRequestCommand command, CancellationToken cancellationToken)
        {
            var loggedInUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email!);

            if(loggedInUser is null) return Errors.User.NotFound;

            var followRequestUser = await _unitOfWork.UserRepository.GetByIdAsync(command.FollowerId);

            if(followRequestUser is null) return Errors.User.NotFound;

            // UPDATE THE NOTIFICATION //

            var notificationToUpdate = await _unitOfWork.NotificationRepository.CheckNotificationExistsAsync(command.FollowerId, loggedInUser.Id, NotificationType.FollowRequest);

            if(notificationToUpdate is null) return Errors.Notifications.NoNotifications;

            // Security Check
            if(notificationToUpdate.Type is not NotificationType.FollowRequest) return Errors.Notifications.NotificationNotFollowingRequest;

            notificationToUpdate.UpdateNotificationMessage($"{followRequestUser.UserName} now follows you.");
            notificationToUpdate.UpdateNotificationType(NotificationType.FollowAccepted);
            notificationToUpdate.UpdateIsReadStatus(false);

            _unitOfWork.NotificationRepository.Update(notificationToUpdate);

            // UPDATE THE FRIENDSHIP //

            var friendshipToAccept = await _unitOfWork.UserRepository.GetFriendShipStatusAsync(command.FollowerId, loggedInUser.Id);
            
            if(friendshipToAccept is null) return Errors.User.FriendShipNotFound;

            // Security Check
            if(friendshipToAccept.Status is not FriendshipStatus.Pending) return Errors.User.FollowRequestNotPending;

            // Update the friendship status to accepted
            friendshipToAccept.UpdateFriendshipStatus(FriendshipStatus.Accepted);

            _unitOfWork.FriendshipRepository.Update(friendshipToAccept);

            // Persits changes to the database
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}