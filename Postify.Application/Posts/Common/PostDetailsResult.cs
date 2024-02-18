using Postify.Domain.Entities;

namespace Postify.Application.Posts.Common
{
    public record PostDetailsResult(
        Guid Id,
        string Description,
        string Image,
        DateTimeOffset CreatedAt,
        List<Comment> Comments,
        bool PostLiked,
        User Poster
    );
}