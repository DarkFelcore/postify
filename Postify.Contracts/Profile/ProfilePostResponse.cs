namespace Postify.Contracts.Profile
{
    public record ProfilePostResponse(
        Guid Id,
        string? Image
    );
}