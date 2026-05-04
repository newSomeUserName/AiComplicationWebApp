using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Exceptions;
using UnifiedAIChat.Application.Common.Interfaces;
using UnifiedAIChat.Application.Common.Interfaces.Auth;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Common.Models;
using UnifiedAIChat.Application.Common.Models.Auth;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(IJwtService jwtService, IPasswordHasher passwordHasher, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<LoginData> RegisterAsync(RegisterCommand registerCommand, CancellationToken ct)
        {

            ArgumentException.ThrowIfNullOrEmpty(registerCommand.Name);
            ArgumentException.ThrowIfNullOrEmpty(registerCommand.Password);
            ArgumentException.ThrowIfNullOrEmpty(registerCommand.Email);

            if (await _userRepository.IfEmailExistsAsync(registerCommand.Email, ct))
            {
                throw new ConflictException($"User with {registerCommand.Email} exists. Try again");
            }

            string passwordHash = _passwordHasher.HashPassword(registerCommand.Password);


            User user = new User() { Name = registerCommand.Name, Email = registerCommand.Email, PasswordHash = passwordHash, Role = Role.User };
            await _userRepository.AddUserAsync(user, ct);

            string accessToken = _jwtService.GenerateToken(new UserTokenPayload() { Id = user.Id.ToString(), Name = registerCommand.Name, Email = registerCommand.Email });
            
            //TODO: Check
            RefreshTokenData rtd = _jwtService.GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken() { UserId = user.Id, TokenHash = rtd.Hash, ExpiresAt = DateTime.UtcNow.AddDays(15), CreatedAt = DateTime.UtcNow }, ct);

            return new LoginData(accessToken, rtd.RawToken, DateTime.UtcNow.AddDays(15));


        }
        public async Task<LoginData> LoginAsync(LoginCommand loginCommand, CancellationToken ct)
        {
            var user = await _userRepository.GetByEmailAsync(loginCommand.Email, ct);

            if (user is null)
            {
                throw new Exception("Not Found"); // TODO : not found exception
            }

            if (!_passwordHasher.VerifyPassword(loginCommand.Password, user.PasswordHash))
            {
                throw new Exception("Incorret Password"); // TODO : incorect password exception
            }

            var userTokenPayload = new UserTokenPayload() { Id = user.Id.ToString(), Name = user.Name, Email = user.Email };

            string accessToken = _jwtService.GenerateToken(userTokenPayload);


            //TODO: Check

            RefreshTokenData rtd = _jwtService.GenerateRefreshToken();
            await _refreshTokenRepository.AddAsync(new RefreshToken() { UserId = user.Id, TokenHash = rtd.Hash, ExpiresAt = DateTime.UtcNow.AddDays(15), CreatedAt = DateTime.UtcNow });


            return new LoginData(accessToken, rtd.RawToken,DateTime.UtcNow.AddDays(15));

        }
        public async Task<LoginData> RefreshAsync(string rawRefreshToken, CancellationToken ct)
        {
            string refreshTokenHash = _jwtService.HashToken(rawRefreshToken);
            RefreshToken usedRefreshToken = await _refreshTokenRepository.GetByHashAsync(refreshTokenHash) ?? throw new Exception("Token could be stollen"); // TODO: create expiredOrStollenTokenException

            User user = usedRefreshToken.User;


            var userTokenPayload = new UserTokenPayload() { Id = user.Id.ToString(), Name = user.Name, Email = user.Email };
            
            string accessToken = _jwtService.GenerateToken(userTokenPayload);
            RefreshTokenData rtd = _jwtService.GenerateRefreshToken();
          
            string newHash = await _refreshTokenRepository.AddAsync(new RefreshToken() { UserId = user.Id, TokenHash = rtd.Hash, ExpiresAt = DateTime.UtcNow.AddDays(15), CreatedAt = DateTime.UtcNow });

            await _refreshTokenRepository.UpdateAsync(usedRefreshToken, newHash, ct);

            return new LoginData(accessToken, rtd.RawToken, DateTime.UtcNow.AddDays(15));

        }
        public async Task LogoutAsync()
        {

        }
        public async Task LogoutAllAsync()
        {

        }

    }
}
