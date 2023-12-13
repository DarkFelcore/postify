using Postify.Contracts.Users;

namespace Postify.Contracts.Posts
{
    public record PostOverviewResponse(
        string Description,
        string Image,
        DateTimeOffset CreatedAt,
        int CommentsCount,
        List<UserPostOverviewResponse> PostLikes,
        UserPostOverviewResponse Poster
    );
}