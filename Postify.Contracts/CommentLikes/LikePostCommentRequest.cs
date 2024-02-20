namespace Postify.Contracts.CommentLikes
{
    public record LikePostCommentRequest(Guid CommentId, Guid UserId);
}