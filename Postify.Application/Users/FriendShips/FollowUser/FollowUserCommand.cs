using ErrorOr;

using MediatR;

namespace Postify.Application.Users.FriendShips.FollowUser
{
    public record FollowUserCommand(
        Guid UserId,
        string? Email
    ): IRequest<ErrorOr<bool>>;
}