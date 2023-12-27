using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

using Postify.Application.Common.Interfaces;


namespace Postify.Application.Common.Hubs
{
    public class NotificationHub : Hub<INotificationHub>
    {
        private readonly IUserConnectionService _userConnectionService;

        public NotificationHub(IUserConnectionService userConnectionService)
        {
            _userConnectionService = userConnectionService;
        }

        [Authorize]
        public override Task OnConnectedAsync()
        {
            var userId = GetUserId();
            var connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(connectionId))
            {
                _userConnectionService.AddPairToUserConnectionMap(userId, connectionId);
            }

            return base.OnConnectedAsync();
        }

        private string? GetUserId()
        {
            HttpContext context = Context.GetHttpContext()!;
            if (context is not null)
            {
                var accessToken = context.Request.QueryString.Value!.Split("=")[1];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

                if (jsonToken is not null)
                {
                    return jsonToken.Claims.FirstOrDefault(x => x.Type == "sub")!.Value;
                }
            }
            return null;
        }
    }
}