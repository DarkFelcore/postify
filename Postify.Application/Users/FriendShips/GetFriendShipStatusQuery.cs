using ErrorOr;

using MediatR;

using Postify.Domain.Entities;

namespace Postify.Application.Users.FriendShips
{
    public record GetFriendShipStatusQuery(
        Guid UserId,
        Guid ProfileId
    ) : IRequest<ErrorOr<string>>;
}