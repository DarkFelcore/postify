using ErrorOr;

using MediatR;

namespace Postify.Application.Posts.Comments
{
    public record CommentPostCommand(
        Guid PostId, 
        string Email, 
        string? ParentCommentId,
        string Description
    ): IRequest<ErrorOr<bool>>;
}