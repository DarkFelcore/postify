namespace Postify.Contracts.Users
{
    public record GetFriendShipStatusRequest(
        Guid UserId,
        Guid ProfileId
    );
}