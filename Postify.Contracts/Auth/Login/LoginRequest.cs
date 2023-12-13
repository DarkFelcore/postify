namespace Postify.Contracts.Auth.Login
{
    public record LoginRequest(
        string EmailOrUsername,
        string Password
    );
}