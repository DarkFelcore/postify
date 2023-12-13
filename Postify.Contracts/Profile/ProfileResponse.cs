using Postify.Contracts.Profile;

namespace Postify.Contracts.Posts
{
    public record ProfileResponse(
        string FirstName,
        string LastName,
        string UserName,
        int FollowersCount,
        int FollowingCount,
        List<ProfilePostResponse> Posts
    );
}