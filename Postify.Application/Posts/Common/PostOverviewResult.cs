using Postify.Domain.Entities;

namespace Postify.Application.Posts.Common
{
    public record PostOverviewResult(
        string Description,
        string Image,
        DateTimeOffset CreatedAt,
        int CommentsCount,
        List<User> PostLikes,
        User Poster
    );
}