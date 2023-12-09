using Mapster;

using Postify.Application.Posts.Common;
using Postify.Contracts.Posts;

namespace Postify.Api.Common.Mappings
{
    public class PostMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PostResult, PostResponse>()
                .Map(dest => dest, src => src);
            
        }
    }
}