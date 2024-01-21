using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Postify.Api.Extensions;
using Postify.Application.Notifications.GetByUserId;
using Postify.Application.Notifications.MarkAsRead;
using Postify.Contracts.Notifications;

namespace Postify.Api.Controllers
{
    public class NotificationsController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public NotificationsController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserNotifications()
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = new GetNotificationsByUserIdQuery(email);
            var result = await _mediator.Send(query);

            var mappedNotifications = result.Value.Select(x => _mapper.Map<NotificationResponse>(x)).ToList();

            return result.Match(
                _ => Ok(mappedNotifications),
                Problem
            );
        }

        [HttpPost("markAsRead")]
        public async Task<IActionResult> MarkAllUserNotificationsAsRead()
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var command = new MarkNotificationsAsReadCommand(email);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                Problem
            );
        }
    }
}