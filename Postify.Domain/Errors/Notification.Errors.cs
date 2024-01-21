using ErrorOr;

namespace Postify.Domain.Errors
{
    public partial class Errors
    {
        public static class Notifications
        {
            public static Error NoNotifications => Error.NotFound(
                code: "Notifications.NoNotifications",
                description: "There are no notifications available. Consider reloading the page."
            );

            public static Error NotificationNotFollowingRequest => Error.NotFound(
                code: "Notifications.NotificationNotFollowingRequest",
                description: "Notification is not a following request"
            );
        }
    }
}