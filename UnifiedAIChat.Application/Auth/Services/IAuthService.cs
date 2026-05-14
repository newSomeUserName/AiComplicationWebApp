using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Auth.Login;
using UnifiedAIChat.Application.Auth.Register;

namespace UnifiedAIChat.Application.Auth.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> RegisterAsync(RegisterCommand registerCommand, CancellationToken ct = default);
        Task<LoginResponse> LoginAsync(LoginCommand loginCommand, CancellationToken ct = default);
        Task<LoginResponse> RefreshAsync(string rawRefreshToken, CancellationToken ct = default);
        Task LogoutAsync(string rawRefreshToken, CancellationToken ct = default);
        Task LogoutAllAsync();
    }
}
