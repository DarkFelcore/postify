using ErrorOr;

using MediatR;

using Postify.Application.Auth.Common;

namespace Postify.Application.Auth.CurrentUser
{
    public record GetCurrentUserQuery(
        string? Email
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}