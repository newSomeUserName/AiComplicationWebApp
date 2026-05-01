using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UnifiedAIChat.Infrastructure.Authentication;

namespace UnifiedAIChat.Api.Extensions
{
    internal static class AuthenticationExtensions
    {
        static public IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme).Configure<IOptions<JwtOptions>>((options, jwtOptions) =>
            {

                var jwtOptionsValue = jwtOptions.Value;
                
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptionsValue.Issuer,
                    ValidAudience = jwtOptionsValue.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionsValue.Key))
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.TryGetValue("access_token", out string? token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }

                };
            });
            return services;
        }
    }
}
