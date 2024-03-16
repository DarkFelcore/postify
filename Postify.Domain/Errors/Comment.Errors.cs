using ErrorOr;

namespace Postify.Domain.Errors
{
    public partial class Errors
    {
        public static class Comments
        {
            public static Error NotFound => Error.NotFound(
                code: "Comments.NotFound",
                description: "Comment could not be found"
            );
            public static Error ParentIdNotFound => Error.NotFound(
                code: "Comments.ParentIdNotFound",
                description: "Parent comment id not found"
            );

            public static Error DeleteOtherCommentNotAllowed => Error.NotFound(
                code: "Comments.DeleteOtherCommentNotAllowed",
                description: "Deleting a comment from someone else is not allowed"
            );
        }
    }
}