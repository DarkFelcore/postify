using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Domain.Entities;
using Postify.Domain.Errors;

namespace Postify.Application.Posts.Comments
{
    public class CommentPostCommandHandler : IRequestHandler<CommentPostCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentPostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(CommentPostCommand command, CancellationToken cancellationToken)
        {
            var parentCommentId = command.ParentCommentId;
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email);

            if(user is null) return Errors.User.NotFound;

            if(!string.IsNullOrEmpty(command.ParentCommentId) && !await _unitOfWork.CommentRepository.CheckParentCommentIdExists(command.ParentCommentId)) 
            {
                return Errors.Comments.ParentIdNotFound;
            }

            var newComment = new Comment(
                id: Guid.NewGuid(), 
                parentCommentId: parentCommentId,
                description: command.Description,
                commentLikes: [],
                postId: command.PostId,
                userId: user.Id
            );

            await _unitOfWork.CommentRepository.AddAsync(newComment);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}