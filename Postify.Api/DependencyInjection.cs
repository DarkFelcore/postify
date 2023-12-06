using Microsoft.AspNetCore.Mvc.Infrastructure;

using Postify.Api.Common;
using Postify.Api.Common.Errors;

namespace Postify.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, PostifyProblemDetailFactory>();
            services.AddMappings();
            services.AddCors(options => options.AddPolicy(name: "CorsPolicy",
                policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            return services;
        }
    }
}