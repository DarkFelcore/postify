namespace Postify.Application.Common.Interfaces
{
    public interface IUserConnectionService
    {
        void AddPairToUserConnectionMap(string userId, string connectionId);
        string? GetUserConnectionId(string userId);
    }
}