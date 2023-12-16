using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities.Enums;

namespace Postify.Application.Users.FriendShips.Helpers
{
    public static class FriendShipHelpers
    {
        public static async Task<string> GetFriendShipStatus(Guid userId, Guid profileId, IUnitOfWork unitOfWork)
        {
            var userFriendShip = await unitOfWork.UserRepository.GetFriendShipStatusAsync(userId, profileId);
            var profileFriendShip = await unitOfWork.UserRepository.GetFriendShipStatusAsync(profileId, userId);

            return userFriendShip == null || userFriendShip.Status == FriendshipStatus.Rejected
                ? profileFriendShip?.Status == FriendshipStatus.Accepted
                    ? "Follow Back"
                    : "Follow"
                : userFriendShip.Status switch
                {
                    FriendshipStatus.Pending => "Pending",
                    FriendshipStatus.Accepted => "Following",
                    _ => "",
                };
        }
    }
}