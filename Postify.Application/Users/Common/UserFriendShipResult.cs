namespace Postify.Application.Users.Common
{
    public record UserFriendShipResult(
        Guid Id,
        string? PictureUrl,
        string UserName,
        string FirstName,
        string LastName,
        string FriendShipStatus
    );
}