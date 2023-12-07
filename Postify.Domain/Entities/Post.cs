using Postify.Domain.Primitives;

namespace Postify.Domain.Entities
{
    public class Post : Entity
    {
        public string Description { get; private set; } = string.Empty;
        public string Link { get; private set; } = string.Empty;
        public byte[]? Image { get; private set; }
        public DateTimeOffset PostedAt { get; private set; }

        // Releations
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }

        public List<Reaction> Reactions { get; set; } = null!;

        public Post() {}

        public Post(Guid id, string description, string link, byte[] image, Guid userId, List<Reaction> reactions) : base(id)
        {
            Description = description;
            Link = link;
            Image = image;
            PostedAt = DateTimeOffset.Now;
            UserId = userId;
            Reactions = reactions;
        }
    }
}