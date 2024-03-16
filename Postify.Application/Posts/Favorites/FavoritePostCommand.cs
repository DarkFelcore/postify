using ErrorOr;

using MediatR;

namespace Postify.Application.Posts.Favorites
{
    public record FavoritePostCommand(
        string Email,
        Guid PostId
    ): IRequest<ErrorOr<bool>>;
}