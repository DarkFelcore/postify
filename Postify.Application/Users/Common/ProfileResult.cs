using Postify.Domain.Entities;

namespace Postify.Application.Users.Common
{
    public record ProfileResult(
        string FirstName,
        string LastName,
        string UserName,
        int FollowersCount,
        int FollowingCount,
        List<Post> Posts
    );
}