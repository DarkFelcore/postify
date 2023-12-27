using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Application.Users.Common;
using Postify.Application.Users.FriendShips.Helpers;
using Postify.Domain.Errors;

namespace Postify.Application.Users.FriendShips.UserFollowings
{
    public class GetUserFollowingsQueryHandler : IRequestHandler<GetUserFollowingsQuery, ErrorOr<List<UserFriendShipResult>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserFollowingsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<UserFriendShipResult>>> Handle(GetUserFollowingsQuery query, CancellationToken cancellationToken)
        {
            var userFollowingsResultList = new List<UserFriendShipResult>();

            var loggedInUserId = (await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email!))!.Id;

            // Followers of the profile user
            var followings = await _unitOfWork.UserRepository.GetUserFollowingsAsync(query.UserId);

            foreach (var following in followings)
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(following.FollowedId);

                if (user is null) return Errors.User.NotFound;

                // Get the friendship status of the logged in user with the profile user followings
                var status = await FriendShipHelpers.GetFriendShipStatus(loggedInUserId, following.FollowedId, _unitOfWork);

                userFollowingsResultList.Add(new UserFriendShipResult(
                    user.Id,
                    user.PictureUrl != null ? Convert.ToBase64String(user.PictureUrl) : null,
                    user.UserName,
                    user.FirstName,
                    user.LastName,
                    status
                ));
            }

            userFollowingsResultList = [.. userFollowingsResultList.OrderBy(u => u.Id == loggedInUserId ? 0 : 1)];

            return userFollowingsResultList;
        }
    }
}