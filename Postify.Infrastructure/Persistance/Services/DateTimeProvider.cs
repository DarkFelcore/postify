using Postify.Application.Common.Interfaces;

namespace Postify.Infrastructure.Persistance.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}