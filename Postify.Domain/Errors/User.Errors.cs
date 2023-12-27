using ErrorOr;

namespace Postify.Domain.Errors
{
    public partial class Errors
    {
        public static class User
        {
            public static Error NotFound => Error.NotFound(
                code: "User.NotFound",
                description: "User not found"
            );

            public static Error FriendShipNotFound => Error.NotFound(
                code: "User.FriendShipNotFound",
                description: "FriendShip not found"
            );
        }
    }
}