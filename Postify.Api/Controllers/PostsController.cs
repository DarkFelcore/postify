using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Postify.Api.Extensions;
using Postify.Application.Posts.Comments;
using Postify.Application.Posts.GetAll;
using Postify.Application.Posts.GetDetails;
using Postify.Application.Posts.Like;
using Postify.Contracts.Comments;
using Postify.Contracts.Posts;

namespace Postify.Api.Controllers
{
    public class PostsController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PostsController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var query = new GetAllPostsQuery();
            var result = await _mediator.Send(query);

            // Map individual posts to postResponses
            var mappedPosts = result.Value.Select(x => _mapper.Map<PostOverviewResponse>(x)).ToList();

            return result.Match(
                result => Ok(mappedPosts),
                Problem
            );
        }

        [HttpGet("{postId:guid}")]
        public async Task<IActionResult> GetPostDetailsAsync([FromRoute] Guid postId)
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = new GetPostDetailsQuery(postId, email);
            var result = await _mediator.Send(query);

            var mappedPostDetails = _mapper.Map<PostDetailsResponse>(result.Value);

            return result.Match(
                result => Ok(mappedPostDetails),
                Problem
            );
        }

        [HttpPost("like/{postId:guid}")]
        public async Task<IActionResult> LikePostAsync([FromRoute] Guid postId) 
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var command = new LikePostCommand(postId, email);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                Problem
            );
        }

        [HttpPost("comment/{postId:guid}")]
        public async Task<IActionResult> CommentPostAsync([FromRoute] Guid postId, [FromBody] CommentPostRequest request)
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var command = _mapper.Map<CommentPostCommand>((request, email, postId));
            var result = await _mediator.Send(command);

            var mappedComment = _mapper.Map<CommentResponse>(result.Value);

            return result.Match(
                _ => Ok(mappedComment),
                Problem
            );
        }
    }
}