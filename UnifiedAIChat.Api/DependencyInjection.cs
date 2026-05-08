using Anthropic;
using Anthropic.Core;
using Microsoft.Extensions.Options;
using UnifiedAIChat.Api.Extensions;
using UnifiedAIChat.Api.Middlewares;
using UnifiedAIChat.Application.Common.Interfaces;
using UnifiedAIChat.Application.Common.Interfaces.AI;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Services.Auth;
using UnifiedAIChat.Application.Services.Chat;
using UnifiedAIChat.Application.Services.Messege;
using UnifiedAIChat.Infrastructure.AI;
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
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("Default_Connection")!;
            builder.Services.AddDbOptions<AppDbContext>(connectionString);

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddJwtAuthentication();

            builder.Services.AddAuthorization();

            builder.Services.AddScoped<IJwtService, JwtService>(); //TODO : recreate to singleton
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IChatRepository, ChatRepository>();


            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IMessegeService, MessegeService>();


            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));


            builder.Services.AddOptions<ApiOptions>()
                .Bind(builder.Configuration.GetSection(ApiOptions.SectionName));



            builder.Services.AddSingleton<AnthropicClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<ApiOptions>>().Value;

                return new AnthropicClient
                {
                    ApiKey = options.ApiKey,
                    MaxRetries = 2,
                    Timeout = TimeSpan.FromSeconds(20)
                };
            });
            builder.Services.AddScoped<IAIChatProvider, ClaudeAIChatProvider>();
        }
    }
}
