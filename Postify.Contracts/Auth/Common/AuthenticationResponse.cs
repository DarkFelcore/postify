namespace Postify.Contracts.Auth.Common
{
    public record AuthenticationResponse(
        Guid Id,
        string PictureUrl,
        string Token
    );
}