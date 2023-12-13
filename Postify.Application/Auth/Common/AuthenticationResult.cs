namespace Postify.Application.Auth.Common
{
    public record AuthenticationResult(
        Guid Id,
        string Token
    );
}