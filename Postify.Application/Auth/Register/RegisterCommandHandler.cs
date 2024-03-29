using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Http;

using Postify.Application.Auth.Common;
using Postify.Application.Common.Interfaces;
using Postify.Domain.Aggregates;
using Postify.Domain.Entities;
using Postify.Domain.Errors;

namespace Postify.Application.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            // Check email already registered
            if((await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email)) is not null)
            {
                return Errors.Auth.DuplicateEmail;
            }

            // Check username already registered
            if((await _unitOfWork.UserRepository.GetUserByUsernameAsync(command.UserName)) is not null)
            {
                return Errors.Auth.DuplicateUserName;
            }

            // Hash the password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            // Create local user object
            var user = new User(
                id: Guid.NewGuid(),
                firstName: command.FirstName,
                lastName: command.LastName,
                userName: command.UserName,
                email: command.Email,
                passwordHash
            );

            // Generate token
            var token = _jwtTokenGenerator.GenerateJWTToken(user);

            // Generate refresh token
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            var mappedUser = MapRefreshTokenInfoToUser(user, refreshToken);

            // Persist the new user
            await _unitOfWork.UserRepository.AddAsync(mappedUser);
            await _unitOfWork.CompleteAsync();

            return new AuthenticationResult(
                user.Id,
                user.PictureUrl != null ? Convert.ToBase64String(user.PictureUrl) : "",
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
    }
}