using ErrorOr;

using MediatR;

namespace Postify.Application.Posts.Like
{
    public record LikePostCommand(Guid PostId, string Email) : IRequest<ErrorOr<bool>>;
}