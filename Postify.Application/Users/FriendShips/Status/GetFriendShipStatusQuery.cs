using ErrorOr;

using MediatR;

using Postify.Application.Users.Common;

namespace Postify.Application.Users.FriendShips.Status
{
    public record GetFriendShipStatusQuery(
        Guid UserId,
        Guid ProfileId
    ) : IRequest<ErrorOr<FriendShipStatusResult>>;
}