using Mapster;

using Microsoft.AspNetCore.Identity.Data;

using Postify.Application.Auth.Common;
using Postify.Application.Auth.Login;
using Postify.Application.Auth.RefreshTokens;
using Postify.Application.Auth.Register;
using Postify.Contracts.Auth.Common;
using Postify.Contracts.Auth.RefreshToken;

namespace Postify.Api.Common.Mappings
{
    public class AuthMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Register Request
            config.NewConfig<RegisterRequest, RegisterCommand>()
                .Map(dest => dest, src => src);

            // Login Request
            config.NewConfig<LoginRequest, LoginQuery>()
                .Map(dest => dest, src => src);

            // Login Response
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src);

            // Refresh Token Request
            config.NewConfig<RefreshTokenRequest, RefreshTokenCommand>()
                .Map(dest => dest, src => src);
        }
    }
}