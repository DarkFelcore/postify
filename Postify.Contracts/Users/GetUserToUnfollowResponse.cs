namespace Postify.Contracts.Users
{
    public record GetUserToUnfollowResponse(
        Guid Id,
        string UserName,
        string? PictureUrl
    );
}