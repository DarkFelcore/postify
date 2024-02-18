using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Application.Posts.Common;
using Postify.Domain.Errors;

namespace Postify.Application.Posts.GetDetails
{
    public class GetPostDetailsQueryHandler : IRequestHandler<GetPostDetailsQuery, ErrorOr<PostDetailsResult>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPostDetailsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<PostDetailsResult>> Handle(GetPostDetailsQuery query, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email);

            if(currentUser is null) return Errors.User.NotFound;

            var post = await _unitOfWork.PostRepository.GetByIdAsync(query.PostId);

            if(post is null) return Errors.Post.NotFound;

            var isPostLiked = await _unitOfWork.PostLikeRepository.CheckPostLikeExistsAsync(post.Id, currentUser.Id) != null;

            return new PostDetailsResult(
                post.Id, 
                post.Description, 
                post.Image != null ? Convert.ToBase64String(post.Image) : string.Empty, 
                post.CreatedAt, 
                post.Comments!,
                isPostLiked,
                post.User
            );
        }
    }
}