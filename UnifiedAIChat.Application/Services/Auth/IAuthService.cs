using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Models.Auth;

namespace UnifiedAIChat.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginData> RegisterAsync(RegisterCommand registerCommand, CancellationToken ct = default);
        Task<LoginData> LoginAsync(LoginCommand loginCommand, CancellationToken ct = default);
        Task<LoginData> RefreshAsync(string rawRefreshToken, CancellationToken ct = default);
        Task LogoutAsync(string rawRefreshToken, CancellationToken ct = default);
        Task LogoutAllAsync();
    }
}
