using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Postify.Domain.Entities;
using Postify.Domain.Entities.Enums;

namespace Postify.Infrastructure.Persistance.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .HasConversion(
                    x => x.ToString(),
                    x => (NotificationType)Enum.Parse(typeof(NotificationType), x)
                );
        }
    }
}