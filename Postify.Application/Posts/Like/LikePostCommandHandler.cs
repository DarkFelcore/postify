using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;
using Postify.Domain.Errors;

namespace Postify.Application.Posts.Like
{
    public class LikePostCommandHandler : IRequestHandler<LikePostCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikePostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(LikePostCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email);

            if(user is null) return Errors.User.NotFound;

            var existingPostLike = await _unitOfWork.PostLikeRepository.CheckPostLikeExistsAsync(command.PostId, user.Id);

            // Post like exists: remove the like from the database
            if(existingPostLike is not null)
            {
                _unitOfWork.PostLikeRepository.Delete(existingPostLike);
            }
            // Post like does not exists: add the like to the database
            else 
            {
                var newPostLike = new PostLike(user.Id, command.PostId);
                await _unitOfWork.PostLikeRepository.AddAsync(newPostLike);
            }

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}