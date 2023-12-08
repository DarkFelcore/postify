using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Postify.Domain.Entities;
using Postify.Domain.Entities.Enums;

namespace Postify.Infrastructure.Persistance.Configurations
{
    public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.HasKey(x => new { x.FollowerId, x.FollowedId });

            builder.Property(x => x.Status)
                .HasConversion(
                    x => x.ToString(),
                    x => (FriendshipStatus)Enum.Parse(typeof(FriendshipStatus), x)
                );
        }
    }
}