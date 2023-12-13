namespace Postify.Contracts.Auth.Register
{
    public record RegisterRequest(
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string Password
    );
}