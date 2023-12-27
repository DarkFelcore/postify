using System.Text;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Postify.Application.Common.Hubs;
using Postify.Application.Common.Interfaces;
using Postify.Application.Common.Services;
using Postify.Infrastructure.Authentication;
using Postify.Infrastructure.Persistance;
using Postify.Infrastructure.Persistance.Repositories;
using Postify.Infrastructure.Persistance.Services;

namespace Postify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuth(configuration)
                .AddPersistance(configuration);
            services.AddSignalR();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IUserConnectionService, UserConnectionService>();
            return services;
        }

        private static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL Server
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), action => action.CommandTimeout(30)));

            // Services - Repositories registration
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)
                    ),

                    ClockSkew = TimeSpan.FromSeconds(0)
                });

            return services;
        }
    }
}