using Postify.Domain.Primitives;

namespace Postify.Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public byte[]? ProfilePicture { get; private set; }

        // Relations
        public List<Post> Posts { get; set; } = null!;
        public List<Friend> Friends { get; set; } = null!;

        public User() {}

        public User(Guid id, string firstName, string lastName, string email, string passwordHash, byte[]? profilePicture, List<Post> posts, List<Friend> friends) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            ProfilePicture = profilePicture;
            Posts = posts;
            Friends = friends;
        }
    }
}