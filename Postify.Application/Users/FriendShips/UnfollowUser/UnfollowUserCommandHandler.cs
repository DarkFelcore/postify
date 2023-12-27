using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
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

            _unitOfWork.FriendshipRepository.Delete(friendShip);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}