using Postify.Contracts.Users;

namespace Postify.Contracts.Posts
{
    public record PostOverviewResponse(
        Guid Id,
        string Description,
        string Image,
        DateTimeOffset CreatedAt,
        int CommentsCount,
        List<UserPostOverviewResponse> PostLikes,
        UserPostOverviewResponse Poster
    );
}