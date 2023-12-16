using System.ComponentModel;

using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Application.Users.Common;
using Postify.Application.Users.FriendShips.Helpers;
using Postify.Domain.Entities.Enums;
using Postify.Domain.Errors;

namespace Postify.Application.Users.FriendShips.ByUserId
{
    public class GetUserFollowersQueryHandler : IRequestHandler<GetUserFollowersQuery, ErrorOr<List<UserFollowerResult>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserFollowersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<UserFollowerResult>>> Handle(GetUserFollowersQuery query, CancellationToken cancellationToken)
        {
            var userFollowersResultList = new List<UserFollowerResult>();

            var loggedInUserId = (await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email!))!.Id;

            // Followers of the profile user
            var followers = await _unitOfWork.UserRepository.GetUserFollowersAsync(query.UserId);

            foreach (var follower in followers)
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(follower.FollowerId);

                if (user is null) return Errors.User.NotFound;

                // Get the friendship status of the logged in user with the profile user followers
                var status = await FriendShipHelpers.GetFriendShipStatus(loggedInUserId, follower.FollowerId, _unitOfWork);

                userFollowersResultList.Add(new UserFollowerResult(
                    user.Id,
                    user.PictureUrl != null ? Convert.ToBase64String(user.PictureUrl) : null,
                    user.UserName,
                    user.FirstName,
                    user.LastName,
                    status
                ));
            }

            return userFollowersResultList;

        }
    }
}