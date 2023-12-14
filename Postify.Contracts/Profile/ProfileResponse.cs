using Postify.Contracts.Profile;

namespace Postify.Contracts.Posts
{
    public record ProfileResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string UserName,
        string PictureUrl,
        int FollowersCount,
        int FollowingCount,
        List<ProfilePostResponse> Posts
    );
}