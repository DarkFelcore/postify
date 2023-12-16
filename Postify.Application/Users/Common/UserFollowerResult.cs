namespace Postify.Application.Users.Common
{
    public record UserFollowerResult(
        Guid Id,
        string? PictureUrl,
        string UserName,
        string FirstName,
        string LastName,
        string FriendShipStatus
    );
}