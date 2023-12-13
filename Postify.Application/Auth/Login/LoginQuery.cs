using ErrorOr;

using MediatR;

using Postify.Application.Auth.Common;

namespace Postify.Application.Auth.Login
{
    public record LoginQuery(
        string EmailOrUsername,
        string Password
    ): IRequest<ErrorOr<AuthenticationResult>>;
}