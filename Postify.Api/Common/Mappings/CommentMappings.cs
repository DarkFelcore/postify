using Mapster;

using Postify.Application.Posts.Comments.Like;
using Postify.Contracts.CommentLikes;
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

            config.NewConfig<(LikePostCommentRequest Request, string Email), LikePostCommentCommand>()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest, src => src.Request);
        }
    }
}