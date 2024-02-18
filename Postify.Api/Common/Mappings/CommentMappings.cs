using Mapster;

using Postify.Contracts.Comments;
using Postify.Domain.Entities;

namespace Postify.Api.Common.Mappings
{
    public class CommentMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Comment, CommentResponse>()
                .Map(dest => dest, src => src);
        }
    }
}