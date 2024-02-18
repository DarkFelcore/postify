namespace Postify.Contracts.Comments
{
    public record CommentPostRequest(
        string Description,
        string? ParentCommentId
    );
}