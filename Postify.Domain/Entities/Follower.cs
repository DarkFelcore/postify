using Postify.Domain.Entities.Enums;

namespace Postify.Domain.Entities
{
    public class Follower
    {
        public FriendshipStatus Status { get; private set; }

        public Guid FollowerId { get; private set; }

        public Guid FollowedId { get; private set; }

        public Follower(Guid followerId, Guid followedId, FriendshipStatus status)
        {
            FollowerId = followerId;
            FollowedId = followedId;
            Status = status;
        }
    }
}