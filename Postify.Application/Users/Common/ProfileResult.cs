using Postify.Domain.Entities;

namespace Postify.Application.Users.Common
{
    public record ProfileResult(
        Guid Id,
        string FirstName,
        string LastName,
        string UserName,
        string? PictureUrl,
        int FollowersCount,
        int FollowingCount,
        List<Post> Posts
    );
}