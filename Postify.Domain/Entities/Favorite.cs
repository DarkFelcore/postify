namespace Postify.Domain.Entities
{
    public class Favorite
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }

        public Favorite(Guid userId, Guid postId)
        {
            UserId = userId;
            PostId = postId;
        }
    }
}