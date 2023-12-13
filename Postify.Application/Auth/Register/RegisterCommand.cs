using ErrorOr;

using MediatR;

using Postify.Application.Auth.Common;

namespace Postify.Application.Auth.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}