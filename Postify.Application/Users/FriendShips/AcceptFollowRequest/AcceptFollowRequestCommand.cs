using ErrorOr;

using MediatR;

namespace Postify.Application.Users.FriendShips.AcceptFollowRequest
{
    public record AcceptFollowRequestCommand(
        string? Email,
        Guid FollowerId
    ): IRequest<ErrorOr<bool>>;
}