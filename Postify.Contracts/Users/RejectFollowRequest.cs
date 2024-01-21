namespace Postify.Contracts.Users
{
    public record RejectFollowRequest(
        Guid FollowerId
    );
}