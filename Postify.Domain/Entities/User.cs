using Postify.Domain.Primitives;

namespace Postify.Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public byte[]? PictureUrl { get; private set; }

        // Relationships
        public List<Comment>? Comments { get; private set; } = null!;
        public List<Post>? Posts { get; private set; } = null!;
        public List<CommentLike>? CommentLikes { get; private set; } = null!;
        public List<PostLike>? PostLikes { get; private set; } = null!;
        public List<Follower>? Friendships { get; private set; } = null!;

        public User()
        {

        }

        public User(Guid id, string firstName, string lastName, string email, string passwordHash, byte[]? pictureUrl, List<Comment>? comments, List<Post>? posts, List<CommentLike>? commentLikes, List<PostLike>? postLikes, List<Follower>? friendships) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PictureUrl = pictureUrl;
            Comments = comments;
            Posts = posts;
            CommentLikes = commentLikes;
            PostLikes = postLikes;
            Friendships = friendships;
        }
    }
}