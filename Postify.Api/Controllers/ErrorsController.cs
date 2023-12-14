using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Postify.Api.Controllers
{
    public class ErrorsController : ApiController
    {
        [AllowAnonymous]
        [Route("/error")]
        public IActionResult Error()
        {
            // Get the exception that occured during the request.
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            return Problem(title: exception?.Message);
        }
    }
}