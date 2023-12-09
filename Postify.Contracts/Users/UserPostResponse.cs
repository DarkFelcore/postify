namespace Postify.Contracts.Users
{
    public record UserPostResponse(
        string PictureUrl,
        string FirstName,
        string LastName
    );
}