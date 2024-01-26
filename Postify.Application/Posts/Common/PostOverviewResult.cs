using Postify.Domain.Entities;

namespace Postify.Application.Posts.Common
{
    public record PostOverviewResult(
        Guid Id,
        string Description,
        string Image,
        DateTimeOffset CreatedAt,
        int CommentsCount,
        List<User> PostLikes,
        User Poster
    );
}