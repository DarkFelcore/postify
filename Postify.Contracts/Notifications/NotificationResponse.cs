namespace Postify.Contracts.Notifications
{
    public record NotificationResponse(
        string Message,
        bool IsRead,
        string Type,
        string CreatedAt,
        Guid SenderId,
        string? FollowRequestUsername,
        string? FollowRequestPictureUrl,
        string? FollowRequestCurrentFriendShipStatus,
        string? FollowRequestAcceptedUsername,
        string? FollowRequestAcceptedPictureUrl
    );
}