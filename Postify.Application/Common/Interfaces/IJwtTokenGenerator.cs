using Microsoft.AspNetCore.Http;

using Postify.Domain.Aggregates;
using Postify.Domain.Entities;

namespace Postify.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateJWTToken(User user);
        RefreshToken GenerateRefreshToken();
    }
}