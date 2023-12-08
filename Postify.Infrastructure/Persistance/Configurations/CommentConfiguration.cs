using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Postify.Domain.Entities;

namespace Postify.Infrastructure.Persistance.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(c => c.CommentLikes)
                .WithOne()
                .HasForeignKey(cl => cl.CommentId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}