using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UnifiedAIChat.Api.AppConfiguration.Models;

namespace UnifiedAIChat.Api.Extensions
{
    internal static class AuthenticationExtensions
    {
        static public AuthenticationBuilder AddJwtAuthentication(this IServiceCollection collection , JwtOptions jwtOptions)
        {
            return collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                .AddJwtBearer(options =>
                                {
                                    options.TokenValidationParameters = new TokenValidationParameters()
                                    {
                                        ValidateIssuer = true,
                                        ValidateAudience = true,
                                        ValidateLifetime = true,
                                        ValidateIssuerSigningKey = true,
                                        ValidIssuer = jwtOptions.Issuer,
                                        ValidAudience = jwtOptions.Audience,
                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
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


                                }
                                );
        }
    }
}
