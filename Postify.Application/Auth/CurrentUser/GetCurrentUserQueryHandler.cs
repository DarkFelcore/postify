using ErrorOr;

using MediatR;

using Postify.Application.Auth.Common;
using Postify.Application.Common.Interfaces;
using Postify.Domain.Errors;

namespace Postify.Application.Auth.CurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public GetCurrentUserQueryHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email!);

            if(user is null) return Errors.User.NotFound;

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user.Id,
                user.PictureUrl != null ? Convert.ToBase64String(user.PictureUrl) : "",
                token
            );
        }
    }
}