using System.Security.Claims;

namespace Postify.Api.Extensions
{
    public class AuthenticationExtensions
    {
        public static string GetEmailByClaimTypesAsync(ClaimsPrincipal claimPrincipal)
        {
            return claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;
        }
    }
}