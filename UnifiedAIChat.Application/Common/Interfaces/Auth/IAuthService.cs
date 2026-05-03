using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Models.Auth;

namespace UnifiedAIChat.Application.Common.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterCommand registerCommand, CancellationToken ct = default);
        Task<string> LoginAsync(LoginCommand loginCommand, CancellationToken ct = default);
        Task RefreshAsync();
        Task LogoutAsync();
        Task LogoutAllAsync();
    }
}
