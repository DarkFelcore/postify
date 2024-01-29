namespace Postify.Contracts.Posts
{
    public record CommentPostRequest(
        string Description,
        string? ParentCommentId
    );
}