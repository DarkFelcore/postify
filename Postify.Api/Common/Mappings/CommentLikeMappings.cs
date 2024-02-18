using Mapster;

using Postify.Contracts.CommentLikes;
using Postify.Domain.Entities;

namespace Postify.Api.Common.Mappings
{
    public class CommentLikeMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CommentLike, CommentLikeResponse>()
                .Map(dest => dest, src => src);
        }
    }
}