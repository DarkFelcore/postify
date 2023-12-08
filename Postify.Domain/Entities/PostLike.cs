namespace Postify.Domain.Entities
{
    public class PostLike
    {
        public Guid UserId { get; private set; }
        public Guid PostId { get; private set; }

        public PostLike(Guid userId, Guid postId)
        {
            UserId = userId;
            PostId = postId;
        }       
    }
}