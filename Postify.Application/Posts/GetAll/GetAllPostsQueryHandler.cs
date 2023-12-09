using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Application.Posts.Common;

namespace Postify.Application.Posts.GetAll
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, ErrorOr<List<PostResult>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPostsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<PostResult>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var postResults = new List<PostResult>();
            var posts = await _unitOfWork.PostRepository.AllAsync();

            foreach (var post in posts)
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(post.UserId);
                var postLikeUsers = await _unitOfWork.UserRepository.GetPostLikeUsers(post.PostLikes!);
                postResults.Add(new PostResult(
                    post.Description,
                    post.Image != null ? Convert.ToBase64String(post.Image) : "",
                    post.CreatedAt,
                    post.Comments!.Count,
                    postLikeUsers,
                    user!
                ));
            }

            return postResults;
        }
    }
}