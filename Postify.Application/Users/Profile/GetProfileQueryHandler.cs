using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Application.Users.Common;
using Postify.Domain.Errors;

namespace Postify.Application.Users.Profile
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ErrorOr<ProfileResult>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProfileQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<ProfileResult>> Handle(GetProfileQuery query, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(query.UserId);

            if(user is null) return Errors.User.NotFound;

            var posts = await _unitOfWork.PostRepository.AllByUserIdAsync(query.UserId);

            var followersCount = (await _unitOfWork.UserRepository.GetUserFollowersAsync(query.UserId)).Count;
            var followingsCount = (await _unitOfWork.UserRepository.GetUserFollowingsAsync(query.UserId)).Count;

            return new ProfileResult(user.Id, user.FirstName, user.LastName, user.UserName, user.PictureUrl != null ? Convert.ToBase64String(user.PictureUrl) : null, followersCount, followingsCount, posts);
        }
    }
}