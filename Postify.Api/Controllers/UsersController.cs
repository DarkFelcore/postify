using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Postify.Application.Users.FriendShips;
using Postify.Application.Users.GetProfile;
using Postify.Contracts.Posts;
using Postify.Contracts.Users;

namespace Postify.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserProfileAsync(Guid userId)
        {
            var query = new GetProfileQuery(userId);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<ProfileResponse>(result)),
                Problem
            );
        }

        [HttpPost("friendship")]
        public async Task<IActionResult> GetFriendShipStatusAsync(GetFriendShipStatusRequest request)
        {
            var query = _mapper.Map<GetFriendShipStatusQuery>(request);
            var result = await _mediator.Send(query);

            return result.Match(
                _ => Ok(result.Value),
                Problem
            );
        }
    }
}