using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Errors;

namespace Postify.Application.Posts.Comments.Delete
{
    public class DeletePostCommentCommandHandler : IRequestHandler<DeletePostCommentCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePostCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(DeletePostCommentCommand command, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdAsync(command.CommentId);

            if(comment is null) return Errors.Comments.NotFound;

            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email);

            if(user is null) return Errors.User.NotFound;

            if(comment.UserId != user.Id) return Errors.Comments.DeleteOtherCommentNotAllowed;

            // Delete child comments first
            var childComments = await _unitOfWork.CommentRepository.GetAllChildComments(comment.Id.ToString());
            foreach (var childComment in childComments)
            {
                var childCommentLikes = await _unitOfWork.CommentLikeRepository.GetAllCommentLikesByCommentIdAsync(childComment.Id);
                foreach (var childCommentLike in childCommentLikes!)
                {
                    // Delete child comment likes from each child comment
                    _unitOfWork.CommentLikeRepository.Delete(childCommentLike);
                }
                // Delete child comment
                _unitOfWork.CommentRepository.Delete(childComment);
            }

            // Delete root comment likes of root comment
            var rootCommentLikes = await _unitOfWork.CommentLikeRepository.GetAllCommentLikesByCommentIdAsync(comment.Id);
            foreach (var commentLike in rootCommentLikes!)
            {
                _unitOfWork.CommentLikeRepository.Delete(commentLike);
            }

            // Delete root comment
            _unitOfWork.CommentRepository.Delete(comment);

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}