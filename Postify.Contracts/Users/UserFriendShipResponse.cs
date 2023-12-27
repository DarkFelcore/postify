namespace Postify.Contracts.Users
{
    public record UserFriendShipResponse(
        Guid Id,
        string? PictureUrl,
        string UserName,
        string FirstName,
        string LastName,
        string FriendShipStatus
    );
}