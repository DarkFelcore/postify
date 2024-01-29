using Mapster;

using Postify.Application.Posts.Comments;
using Postify.Application.Posts.Common;
using Postify.Contracts.Posts;
using Postify.Contracts.Profile;
using Postify.Domain.Entities;

namespace Postify.Api.Common.Mappings
{
    public class PostMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PostOverviewResult, PostOverviewResponse>()
                .Map(dest => dest, src => src);

            config.NewConfig<Post, ProfilePostResponse>()
                .Map(dest => dest.Image, src => Convert.ToBase64String(src.Image))
                .Map(dest => dest, src => src);

            config.NewConfig<(CommentPostRequest Request, string Email, Guid PostId), CommentPostCommand>()
                .Map(dest => dest.PostId, src => src.PostId)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest, src => src.Request);
        }
    }
}