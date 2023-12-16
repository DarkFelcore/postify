namespace Postify.Contracts.Users
{
    public record UserFollowerResponse(
        Guid Id,
        string? PictureUrl,
        string UserName,
        string FirstName,
        string LastName,
        string FriendShipStatus
    );
}