namespace Postify.Contracts.Auth.RefreshToken
{
    public record RefreshTokenRequest(
        string AccessToken, 
        string RefreshToken
    );
}