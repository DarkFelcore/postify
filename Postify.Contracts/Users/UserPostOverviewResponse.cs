namespace Postify.Contracts.Users
{
    public record UserPostOverviewResponse(
        string PictureUrl,
        string UserName
    );
}