using Postify.Contracts.Comments;
using Postify.Contracts.Users;

namespace Postify.Contracts.Posts
{
    public record PostDetailsResponse(
        Guid Id,
        string Description,
        string Image,
        DateTimeOffset CreatedAt,
        List<CommentResponse> Comments,
        bool PostLiked,
        UserPostOverviewResponse Poster
    );
}