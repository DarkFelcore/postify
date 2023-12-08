using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Postify.Domain.Entities;

namespace Postify.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            builder
                .HasMany(u => u.CommentLikes)
                .WithOne()
                .HasForeignKey(cl => cl.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(u => u.PostLikes)
                .WithOne()
                .HasForeignKey(cl => cl.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(x => x.Friendships)
                .WithOne()
                .HasForeignKey(x => x.FollowedId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}