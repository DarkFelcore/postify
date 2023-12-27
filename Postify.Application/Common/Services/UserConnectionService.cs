using Postify.Application.Common.Interfaces;

namespace Postify.Application.Common.Services
{
    public class UserConnectionService : IUserConnectionService
    {
        private readonly Dictionary<string, string> _userConnectionMap = new();

        public void AddPairToUserConnectionMap(string userId, string connectionId)
        {
            _userConnectionMap.Remove(userId);
            _userConnectionMap.Add(userId, connectionId);
        }

        public string? GetUserConnectionId(string userId)
        {
            return _userConnectionMap.ContainsKey(userId) ? _userConnectionMap[userId] : null;
        }
    }
}