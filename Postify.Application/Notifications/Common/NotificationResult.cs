namespace Postify.Application.Notifications.Common
{
    public record NotificationResult(
        string Message,
        bool IsRead,
        string Type,
        string CreatedAt,
        Guid SenderId,
        string? FollowRequestUsername = null,
        string? FollowRequestPictureUrl = null,
        string? FollowRequestCurrentFriendShipStatus = null,
        string? FollowRequestAcceptedUsername = null,
        string? FollowRequestAcceptedPictureUrl = null
    );
}