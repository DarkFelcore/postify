using ErrorOr;

using MediatR;

using Postify.Application.Users.Common;

namespace Postify.Application.Users.FriendShips.ByUserId
{
    public record GetUserFollowersQuery(
        Guid UserId,
        string? Email
    ): IRequest<ErrorOr<List<UserFollowerResult>>>;
}