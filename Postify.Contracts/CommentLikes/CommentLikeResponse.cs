namespace Postify.Contracts.CommentLikes
{
    public record CommentLikeResponse(
        Guid UserId,
        Guid CommentId
    );
}