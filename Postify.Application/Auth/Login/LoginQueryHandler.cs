using System.Net.Mail;

using ErrorOr;

using MediatR;

using Postify.Application.Auth.Common;
using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;
using Postify.Domain.Errors;

namespace Postify.Application.Auth.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Check user based on username or email
            User? user = IsValidEmail(query.EmailOrUsername)
                ? await _unitOfWork.UserRepository.GetUserByEmailAsync(query.EmailOrUsername)
                : await _unitOfWork.UserRepository.GetUserByUsernameAsync(query.EmailOrUsername);

            if (user is null)
            {
                return Errors.Auth.InvalidCredentials;
            }

            // Verify the user password
            if(!BCrypt.Net.BCrypt.Verify(query.Password, user.PasswordHash))
            {
                return Errors.Auth.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user.Id,
                user.PictureUrl != null ? Convert.ToBase64String(user.PictureUrl) : "",
                token
            );
        }

        private static bool IsValidEmail(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}