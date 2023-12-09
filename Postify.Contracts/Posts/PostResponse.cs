using Postify.Contracts.Users;

namespace Postify.Contracts.Posts
{
    public record PostResponse(
        string Description,
        string Image,
        DateTimeOffset CreatedAt,
        int CommentsCount,
        List<UserPostResponse> PostLikes,
        UserPostResponse Poster
    );
}