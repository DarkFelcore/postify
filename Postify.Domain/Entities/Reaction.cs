using Postify.Domain.Primitives;

namespace Postify.Domain.Entities
{
    public class Reaction : Entity
    {
        public bool Liked { get; private set; } = false;
        public string Comment { get; private set; } = string.Empty;

        // Relations
        public Post Post { get; set; } = null!;
        public Guid PostId { get; set; }

        public Reaction() {}

        public Reaction(Guid id, bool liked, string comment, Guid postId) : base(id)
        {
            Liked = liked;
            Comment = comment;
            PostId = postId;
        }
    }
}