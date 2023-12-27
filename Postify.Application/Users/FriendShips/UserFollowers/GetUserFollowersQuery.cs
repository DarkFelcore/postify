using ErrorOr;

using MediatR;

using Postify.Application.Users.Common;

namespace Postify.Application.Users.FriendShips.UserFollowers
{
    public record GetUserFollowersQuery(
        Guid UserId,
        string? Email
    ) : IRequest<ErrorOr<List<UserFriendShipResult>>>;
}