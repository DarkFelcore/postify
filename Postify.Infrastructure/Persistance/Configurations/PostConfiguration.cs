using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Postify.Domain.Entities;

namespace Postify.Infrastructure.Persistance.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(p => p.PostLikes)
                .WithOne()
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}