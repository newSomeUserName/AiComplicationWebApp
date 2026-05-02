using UnifiedAIChat.Api.Extensions;
using UnifiedAIChat.Application.Common.Interfaces;
using UnifiedAIChat.Application.Common.Interfaces.Auth;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Services;
using UnifiedAIChat.Infrastructure.Authentication;
using UnifiedAIChat.Infrastructure.Persistence;
using UnifiedAIChat.Infrastructure.Persistence.Repositories;
using UnifiedAIChat.Infrastructure.Security;

namespace UnifiedAIChat.Api
{
    internal static class DependencyInjection
    {
        public static void InitializeInjections(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("Default_Connection")!;
            builder.Services.AddDbOptions<AppDbContext>(connectionString);

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddJwtAuthentication();

            builder.Services.AddAuthorization();

            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}
