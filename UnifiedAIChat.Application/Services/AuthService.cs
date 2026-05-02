using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;

namespace UnifiedAIChat.Application.Services
{
    public class AuthService
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
        public async Task RegisterAsync(string email, string password, CancellationToken ct)
        {

            ArgumentException.ThrowIfNullOrEmpty(email);
            ArgumentException.ThrowIfNullOrEmpty(password);

            if (await _userRepository.IfEmailExistsAsync(email,ct))
            {
                throw new Exception("User Exists"); // TODO: UserExistsException 
            }

            string passwordHash = _passwordHasher.HashPassword(password);




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
