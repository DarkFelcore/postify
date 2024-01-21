using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Postify.Api.Extensions;
using Postify.Application.Users.FriendShips.UserFollowers;
using Postify.Application.Users.FriendShips.Status;
using Postify.Application.Users.Profile;
using Postify.Contracts.Posts;
using Postify.Contracts.Users;
using Postify.Application.Users.FriendShips.UserFollowings;
using Postify.Application.Users.FriendShips.UnfollowUser;
using Postify.Application.Users.FriendShips.FollowUser;
using Postify.Application.Users.FriendShips.AcceptFollowRequest;
using Postify.Application.Users.FriendShips.RejectFollowRequest;

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

        [HttpGet("{userId:guid}/followers")]
        public async Task<IActionResult> GetUserFollowersAsync(Guid userId)
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = new GetUserFollowersQuery(userId, email);
            var result = await _mediator.Send(query);

            var mappedUserFollowers = result.Value.Select(x => _mapper.Map<UserFriendShipResponse>(x)).ToList();

            return result.Match(
                result => Ok(mappedUserFollowers),
                Problem
            );
        }

        [HttpGet("{userId:guid}/followings")]
        public async Task<IActionResult> GetUserFollowingsAsync(Guid userId)
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = new GetUserFollowingsQuery(userId, email);
            var result = await _mediator.Send(query);

            var mappedUserFollowers = result.Value.Select(x => _mapper.Map<UserFriendShipResponse>(x)).ToList();

            return result.Match(
                result => Ok(mappedUserFollowers),
                Problem
            );
        }

        [HttpPost("friendship")]
        public async Task<IActionResult> GetFriendShipStatusAsync(GetFriendShipStatusRequest request)
        {
            var query = _mapper.Map<GetFriendShipStatusQuery>(request);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<FriendShipStatusResponse>(result)),
                Problem
            );
        }

        [HttpPost("follow")]
        public async Task<IActionResult> FollowUserAsync(FollowUserRequest request)
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var command = _mapper.Map<FollowUserCommand>((email, request));
            var result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                Problem
            );
        }

        [HttpPost("follow/accepted")]
        public async Task<IActionResult> AcceptFollowRequestAsync(AcceptFollowRequest request)
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var command = _mapper.Map<AcceptFollowRequestCommand>((email, request));
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                Problem
            );
        }

        [HttpPost("follow/rejected")]
        public async Task<IActionResult> RejectFollowRequestAsync(RejectFollowRequest request)
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var command = _mapper.Map<RejectFollowRequestCommand>((email, request));
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                Problem
            );
        }


        [HttpDelete("unfollow/{userId:guid}")]
        public async Task<IActionResult> UnfollowUserAsync(Guid userId)
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var command = new UnfollowUserCommand(userId, email);
            var result = await _mediator.Send(command);

            return result.Match(
                result => NoContent(),
                Problem
            );
        }
    }
}