using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Application.Users.Common;
using Postify.Application.Users.FriendShips.Helpers;
using Postify.Domain.Entities.Enums;

namespace Postify.Application.Users.FriendShips.Status
{
    public class GetFriendShipStatusQueryHandler : IRequestHandler<GetFriendShipStatusQuery, ErrorOr<FriendShipStatusResult>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFriendShipStatusQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<FriendShipStatusResult>> Handle(GetFriendShipStatusQuery query, CancellationToken cancellationToken)
        {
            return new FriendShipStatusResult(await FriendShipHelpers.GetFriendShipStatus(query.UserId, query.ProfileId, _unitOfWork));
        }
    }
}