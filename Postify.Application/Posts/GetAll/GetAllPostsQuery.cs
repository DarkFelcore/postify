using ErrorOr;

using MediatR;

using Postify.Application.Posts.Common;

namespace Postify.Application.Posts.GetAll
{
    public record GetAllPostsQuery() : IRequest<ErrorOr<List<PostResult>>>;
}