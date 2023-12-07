using Postify.Domain.Primitives;

namespace Postify.Domain.Entities
{
    public class Friend : Entity
    {
        public bool IsFollowing { get; private set; } = false;

        // Releations
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }

        public Friend() {}

        public Friend(bool isFollowing, Guid id, Guid userId) : base(id)
        {
            IsFollowing = isFollowing; 
            UserId = userId;
        }
    }
}