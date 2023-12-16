using Mapster;

using Postify.Application.Users.Common;
using Postify.Application.Users.FriendShips.Status;
using Postify.Contracts.Users;
using Postify.Domain.Entities;

namespace Postify.Api.Common.Mappings
{
    public class UserMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserPostOverviewResponse>()
                .Map(dest => dest.PictureUrl, src => src.PictureUrl != null ? Convert.ToBase64String(src.PictureUrl) : "")
                .Map(dest => dest, src => src);

            config.NewConfig<GetFriendShipStatusRequest, GetFriendShipStatusQuery>()
                .Map(dest => dest, src => src);

            config.NewConfig<FriendShipStatusResult, FriendShipStatusResponse>()
                .Map(dest => dest, src => src);

            config.NewConfig<UserFollowerResult, UserFollowerResponse>()
                .Map(dest => dest, src => src);
        }
    }
}