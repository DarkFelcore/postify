using ErrorOr;

using MediatR;

using Postify.Application.Auth.Common;

namespace Postify.Application.Auth.RefreshTokens
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<ErrorOr<AuthenticationResult>>;
}