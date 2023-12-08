using Postify.Domain.Primitives;

namespace Postify.Domain.Entities
{
    public class Comment : Entity
    {
        public string? ParentCommentId { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

        // Relationships
        public List<CommentLike>? CommentLikes { get; private set; } = null!;

        public Guid PostId { get; private set; }
        public Post Post { get; set; } = null!;

        public Guid UserId { get; private set; }
        public User User { get; set; } = null!;

        public Comment()
        {

        }

        public Comment(Guid id, string? parentCommentId, string description, List<CommentLike>? commentLikes, Guid postId, Guid userId) : base(id)
        {
            ParentCommentId = parentCommentId;
            Description = description;
            CommentLikes = commentLikes;
            PostId = postId;
            UserId = userId;
        }
    }
}