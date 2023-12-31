using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Postify.Application.Posts.GetAll;
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
    }
}