using ErrorOr;

using MediatR;

namespace Postify.Application.Users.FriendShips.UnfollowUser
{
    public record UnfollowUserCommand(
        Guid UserId,
        string? Email
    ) : IRequest<ErrorOr<bool>>;
}