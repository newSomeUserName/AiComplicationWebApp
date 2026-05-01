using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces;
using UnifiedAIChat.Application.Common.Models;

namespace UnifiedAIChat.Infrastructure.Authentication
{
    public class JwtService : IJwtService //TODO: to think about renaming of service to TokenService
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
                new Claim(ClaimTypes.NameIdentifier, userTokenPayload.Id), //JwtRegisteredClaimNames.Sub
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
        public RefreshTokenData GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            var rawToken = Convert.ToHexString(randomBytes);
            var hash = HashToken(rawToken);

            return new RefreshTokenData { Hash = hash , RawToken = rawToken};
        }
            
        public string HashToken(string rawToken)
        {
            var bytes = Encoding.UTF8.GetBytes(rawToken);
            var hash = SHA256.HashData(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
