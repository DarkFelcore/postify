namespace Postify.Domain.Entities
{
    public class CommentLike
    {
        public Guid UserId { get; private set; }
        public Guid CommentId { get; private set; }

        public CommentLike(Guid userId, Guid commentId)
        {
            UserId = userId;
            CommentId = commentId;
        }
    }
}