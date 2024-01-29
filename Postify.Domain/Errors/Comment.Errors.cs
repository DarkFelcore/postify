using ErrorOr;

namespace Postify.Domain.Errors
{
    public partial class Errors
    {
        public static class Comments
        {
            public static Error ParentIdNotFound => Error.NotFound(
                code: "Comments.ParentIdNotFound",
                description: "Parent comment id not found"
            );
        }
    }
}