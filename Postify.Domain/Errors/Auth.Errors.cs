using ErrorOr;

namespace Postify.Domain.Errors
{
    public partial class Errors
    {
        public static class Auth
        {
            public static Error InvalidCredentials => Error.Unauthorized(
                code: "Auth.InvalidCredentials",
                description: "Invalid credentials."
            );

            public static Error DuplicateUserName => Error.Validation(
                code: "Auth.DuplicateUserName",
                description: "The specified username is already in use."
            );

            public static Error DuplicateEmail => Error.Validation(
                code: "Auth.DuplicateEmail",
                description: "The specified email is already in use."
            );
        }
    }
}