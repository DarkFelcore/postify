using ErrorOr;

using MediatR;

using Postify.Application.Posts.Common;

namespace Postify.Application.Posts.GetDetails
{
    public record GetPostDetailsQuery(
        Guid PostId,
        string Email
    ): IRequest<ErrorOr<PostDetailsResult>>;
}