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
        public AuthService(IJwtService jwtService, IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }
        public async Task<string> RegisterAsync(RegisterCommand registerCommand, CancellationToken ct)
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

            string token = _jwtService.GenerateToken(new UserTokenPayload() { Id = user.Id.ToString() ,Name = registerCommand.Name, Email = registerCommand.Email});
        

           
           
            return token;


        }
        public async Task LoginAsync()
        {


        }
        public async Task RefreshAsync()
        {

        }
        public async Task LogoutAsync()
        {

        }
        public async Task LogoutAllAsync()
        {

        }

    }
}
