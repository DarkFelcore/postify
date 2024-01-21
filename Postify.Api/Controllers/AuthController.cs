using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;

using Postify.Api.Extensions;
using Postify.Application.Auth.CurrentUser;
using Postify.Application.Auth.Login;
using Postify.Application.Auth.RefreshTokens;
using Postify.Application.Auth.Register;
using Postify.Contracts.Auth.Common;
using Postify.Contracts.Auth.Login;
using Postify.Contracts.Auth.RefreshToken;
using Postify.Contracts.Auth.Register;

namespace Postify.Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public AuthController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = new GetCurrentUserQuery(email);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            var command = _mapper.Map<RefreshTokenCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }
    }
}