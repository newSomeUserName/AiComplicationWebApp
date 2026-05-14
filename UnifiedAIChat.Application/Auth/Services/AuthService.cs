using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Auth.Login;
using UnifiedAIChat.Application.Auth.Register;
using UnifiedAIChat.Application.Common.Exceptions;
using UnifiedAIChat.Application.Common.Interfaces;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Common.Models;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Auth.Services
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
        public async Task<LoginResponse> RegisterAsync(RegisterCommand registerCommand, CancellationToken ct)
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

            await _refreshTokenRepository.AddAsync(new RefreshToken() { FamilyId = Guid.NewGuid(), UserId = user.Id, TokenHash = rtd.Hash, ExpiresAt = DateTime.UtcNow.AddDays(15), CreatedAt = DateTime.UtcNow }, ct);

            return new LoginResponse(accessToken, rtd.RawToken, DateTime.UtcNow.AddDays(15));


        }
        public async Task<LoginResponse> LoginAsync(LoginCommand loginCommand, CancellationToken ct)
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


            //TODO : GENERATE NEW FAMILY ID TO REFRESH TOKEN

            var userTokenPayload = new UserTokenPayload() { Id = user.Id.ToString(), Name = user.Name, Email = user.Email };

            string accessToken = _jwtService.GenerateToken(userTokenPayload);


            //TODO: Check

            RefreshTokenData rtd = _jwtService.GenerateRefreshToken();
            await _refreshTokenRepository.AddAsync(new RefreshToken() { UserId = user.Id,FamilyId = Guid.NewGuid() ,TokenHash = rtd.Hash, ExpiresAt = DateTime.UtcNow.AddDays(15), CreatedAt = DateTime.UtcNow });


            return new LoginResponse(accessToken, rtd.RawToken, DateTime.UtcNow.AddDays(15));

        }
        public async Task<LoginResponse> RefreshAsync(string rawRefreshToken, CancellationToken ct)
        {
            string refreshTokenHash = _jwtService.HashToken(rawRefreshToken);
            RefreshToken usedRefreshToken = await _refreshTokenRepository.GetByHashAsync(refreshTokenHash) ?? throw new Exception("Token could be stollen"); // TODO: create expiredOrStollenTokenException

            User user = usedRefreshToken.User;


            var userTokenPayload = new UserTokenPayload() { Id = user.Id.ToString(), Name = user.Name, Email = user.Email };

            string accessToken = _jwtService.GenerateToken(userTokenPayload);
            RefreshTokenData rtd = _jwtService.GenerateRefreshToken();

            string newHash = await _refreshTokenRepository.AddAsync(new RefreshToken() { UserId = user.Id, FamilyId = usedRefreshToken.FamilyId, TokenHash = rtd.Hash, ExpiresAt = DateTime.UtcNow.AddDays(15), CreatedAt = DateTime.UtcNow });

            await _refreshTokenRepository.UpdateAsync(usedRefreshToken, newHash, ct);

            return new LoginResponse(accessToken, rtd.RawToken, DateTime.UtcNow.AddDays(15));

        }
        public async Task LogoutAsync(string rawRefreshToken, CancellationToken ct) 
        {

            string refreshTokenHash = _jwtService.HashToken(rawRefreshToken);

            RefreshToken usedRefreshToken = await _refreshTokenRepository.GetByHashAsync(refreshTokenHash,ct) ?? throw new Exception("Token could be stollen"); // TODO: create expiredOrStollenTokenException
            if (usedRefreshToken is null)
                return;

            await _refreshTokenRepository.RevokeFamilyTokenAsync(usedRefreshToken.FamilyId, ct);

        }
        public async Task LogoutAllAsync()
        {

        }

    }
}
