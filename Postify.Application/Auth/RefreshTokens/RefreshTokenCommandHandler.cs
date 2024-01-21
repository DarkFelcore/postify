using System.IdentityModel.Tokens.Jwt;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Http;

using Postify.Application.Auth.Common;
using Postify.Application.Common.Interfaces;
using Postify.Domain.Aggregates;
using Postify.Domain.Entities;
using Postify.Domain.Errors;

namespace Postify.Application.Auth.RefreshTokens
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var userId = GetUserIdFromJwtToken(command.AccessToken);

            if(userId is null) return Errors.User.NotFound;

            var user = await _unitOfWork.UserRepository.GetByIdAsync(Guid.Parse(userId));

            if(user is null) return Errors.User.NotFound;

            if(!user.Token!.Equals(command.RefreshToken))
            {
                return Errors.Auth.InvalidRefreshToken;
            }
            else if (user.TokenExipreDate < DateTime.Now)
            {
                return Errors.Auth.ExpiredRefreshToken;
            }

            var token = _jwtTokenGenerator.GenerateJWTToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            var mappedUser = MapRefreshTokenInfoToUser(user, refreshToken);
            _unitOfWork.UserRepository.Update(mappedUser);
            await _unitOfWork.CompleteAsync();

            return new AuthenticationResult(
                user.Id, 
                user.PictureUrl != null ? Convert.ToBase64String(user.PictureUrl) : string.Empty, 
                token,
                refreshToken.Token
            );
        }

        private static User MapRefreshTokenInfoToUser(User user, RefreshToken refreshToken)
        {
            user.Token = refreshToken.Token;
            user.TokenCreateDate = refreshToken.Created;
            user.TokenExipreDate = refreshToken.Expired;
            return user;
        }

        private static string? GetUserIdFromJwtToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

            return jsonToken?.Claims.First(claim => claim.Type == "sub").Value;
        }
    }
}