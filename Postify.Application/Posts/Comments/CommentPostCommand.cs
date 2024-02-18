using ErrorOr;

using MediatR;

using Postify.Domain.Entities;

namespace Postify.Application.Posts.Comments
{
    public record CommentPostCommand(
        Guid PostId, 
        string Email, 
        string? ParentCommentId,
        string Description
    ): IRequest<ErrorOr<Comment>>;
}