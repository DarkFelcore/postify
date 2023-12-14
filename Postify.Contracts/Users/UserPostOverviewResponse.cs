namespace Postify.Contracts.Users
{
    public record UserPostOverviewResponse(
        Guid Id,
        string PictureUrl,
        string UserName
    );
}