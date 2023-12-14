using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Mvc.Diagnostics;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;
using Postify.Domain.Entities.Enums;

namespace Postify.Application.Users.FriendShips
{
    public class GetFriendShipStatusQueryHandler : IRequestHandler<GetFriendShipStatusQuery, ErrorOr<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFriendShipStatusQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
 
        public async Task<ErrorOr<string>> Handle(GetFriendShipStatusQuery query, CancellationToken cancellationToken)
        {
            var userFriendShip = await _unitOfWork.UserRepository.GetFriendShipStatusAsync(query.UserId, query.ProfileId);

            return userFriendShip == null || userFriendShip.Status == FriendshipStatus.Rejected
                ? (ErrorOr<string>)"Follow"
                : (ErrorOr<string>)(userFriendShip.Status switch
            {
                FriendshipStatus.Pending => "Pending",
                FriendshipStatus.Accepted => "Following",
                _ => ""
            });
        }
    }
}