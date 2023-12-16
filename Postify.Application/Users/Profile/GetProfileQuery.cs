using ErrorOr;

using MediatR;

using Postify.Application.Users.Common;

namespace Postify.Application.Users.Profile
{
    public record GetProfileQuery(
        Guid UserId
    ): IRequest<ErrorOr<ProfileResult>>;
}