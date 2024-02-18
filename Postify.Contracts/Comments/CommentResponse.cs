using Postify.Contracts.CommentLikes;
using Postify.Contracts.Users;

namespace Postify.Contracts.Comments
{
    public record CommentResponse(
        Guid Id,
        string? ParentCommentId,
        string Description,
        DateTimeOffset CreatedAt,
        List<CommentLikeResponse> CommentLikes,
        UserPostOverviewResponse User
    );
}