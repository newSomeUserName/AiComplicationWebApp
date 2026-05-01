using UnifiedAIChat.Api.Extensions;
using UnifiedAIChat.Infrastructure.Authentication;
using UnifiedAIChat.Infrastructure.Persistence;

namespace UnifiedAIChat.Api
{
    internal static class DependencyInjection
    {
        public static void InitializeInjections(this WebApplicationBuilder builder)
        {

            var connectionString = builder.Configuration.GetConnectionString("Default_Connection")!;
            builder.Services.AddDbOptions<AppDbContext>(connectionString);

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddJwtAuthentication();

            builder.Services.AddAuthorization();

        }
    }
}
