using Postify.Domain.Primitives;

namespace Postify.Domain.Entities
{
    public class Post : Entity
    {
        public string Description { get; private set; } = string.Empty;
        public string Link { get; private set; } = string.Empty;
        public byte[] Image { get; private set; } = [];
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

        // Relationships
        public List<Comment>? Comments { get; private set; } = null!;
        public List<PostLike>? PostLikes { get; private set; } = null!;
        public List<Favorite>? Favorites { get; private set; } = null!;

        public Guid UserId { get; private set; }
        public User User { get; set; } = null!;

        public Post()
        {
        }

        public Post(Guid id, string description, string link, byte[] image, Guid userId, List<Comment>? comments, List<PostLike>? postLikes, List<Favorite>? favorites = null) : base(id)
        {
            Description = description;
            Link = link;
            Image = image;
            UserId = userId;
            Comments = comments;
            PostLikes = postLikes;
            Favorites = favorites;
        }
    }
}