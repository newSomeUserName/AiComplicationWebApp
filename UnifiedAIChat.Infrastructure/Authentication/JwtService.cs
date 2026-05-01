using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces;
using UnifiedAIChat.Application.Common.Models;

namespace UnifiedAIChat.Infrastructure.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(JwtOptions options)
        {
            _jwtOptions = options;
        }
        public string GenerateToken(UserTokenPayload userTokenPayload)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userTokenPayload.Id),
                new Claim(ClaimTypes.Name, userTokenPayload.Name),
                new Claim(ClaimTypes.Email, userTokenPayload.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
                claims: claims,
                signingCredentials: credentials
                );

       
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashToken(string rawToken)
        {
            throw new NotImplementedException();
        }
    }
}
