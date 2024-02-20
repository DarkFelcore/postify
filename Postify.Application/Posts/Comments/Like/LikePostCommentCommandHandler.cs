using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;
using Postify.Domain.Errors;

namespace Postify.Application.Posts.Comments.Like
{
    public class LikePostCommentCommandHandler : IRequestHandler<LikePostCommentCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikePostCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(LikePostCommentCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email);

            if(user is null) return Errors.User.NotFound;

            // Check if there is already a comment like available
            var existingCommentLike = await _unitOfWork.CommentLikeRepository.CheckCommentLikeExistsAsync(command.CommentId, command.UserId);

            // Remove the comment like
            if (existingCommentLike is not null)
            {
                _unitOfWork.CommentLikeRepository.Delete(existingCommentLike);
            }
            // Add the comment like
            else 
            {
                var newCommentLike = new CommentLike(command.UserId, command.CommentId);
                await _unitOfWork.CommentLikeRepository.AddAsync(newCommentLike);
            }

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}