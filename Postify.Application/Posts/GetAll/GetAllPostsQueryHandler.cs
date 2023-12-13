using ErrorOr;

using MediatR;

using Postify.Application.Common.Interfaces;
using Postify.Application.Posts.Common;

namespace Postify.Application.Posts.GetAll
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, ErrorOr<List<PostOverviewResult>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPostsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<PostOverviewResult>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var postResults = new List<PostOverviewResult>();
            var posts = await _unitOfWork.PostRepository.AllAsync();

            foreach (var post in posts)
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(post.UserId);
                var postLikeUsers = await _unitOfWork.UserRepository.GetPostLikeUsers(post.PostLikes!);
                postResults.Add(new PostOverviewResult(
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