using ErrorOr;

using MediatR;

namespace Postify.Application.Posts.Comments.Delete
{
    public record DeletePostCommentCommand(
        Guid CommentId,
        string Email
    ): IRequest<ErrorOr<bool>>;
}