using Mapster;

using Postify.Application.Notifications.Common;
using Postify.Contracts.Notifications;

namespace Postify.Api.Common.Mappings
{
    public class NotificationMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<NotificationResult, NotificationResponse>()
                .Map(dest => dest, src => src);
        }
    }
}