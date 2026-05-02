using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Models.Auth;

namespace UnifiedAIChat.Application.Common.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterCommand registerCommand, CancellationToken ct);
        Task LoginAsync();
        Task RefreshAsync();
        Task LogoutAsync();
        Task LogoutAllAsync();
    }
}
