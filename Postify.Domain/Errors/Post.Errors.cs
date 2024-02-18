using ErrorOr;

namespace Postify.Domain.Errors
{
    public partial class Errors
    {
        public static class Post
        {
            public static Error NotFound => Error.NotFound(
                code: "Posts.NotFound",
                description: "No post found."
            );
        }
    }
}