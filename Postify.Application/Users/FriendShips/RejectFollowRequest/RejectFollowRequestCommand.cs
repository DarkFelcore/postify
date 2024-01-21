using ErrorOr;

using MediatR;

namespace Postify.Application.Users.FriendShips.RejectFollowRequest
{
    public record RejectFollowRequestCommand(
        string? Email,
        Guid FollowerId
    ): IRequest<ErrorOr<bool>>;
}