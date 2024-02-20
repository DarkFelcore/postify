using ErrorOr;

using MediatR;

namespace Postify.Application.Posts.Comments.Like
{
    public record LikePostCommentCommand(
        Guid CommentId,
        Guid UserId,
        string Email
    ) : IRequest<ErrorOr<bool>>;
}