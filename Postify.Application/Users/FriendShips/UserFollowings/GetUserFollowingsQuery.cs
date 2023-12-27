using ErrorOr;

using MediatR;

using Postify.Application.Users.Common;

namespace Postify.Application.Users.FriendShips.UserFollowings
{
    public record GetUserFollowingsQuery(
        Guid UserId,
        string? Email
    ) : IRequest<ErrorOr<List<UserFriendShipResult>>>;
}