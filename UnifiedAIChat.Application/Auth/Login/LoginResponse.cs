using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Auth.Login
{
    public record LoginResponse(string AccessToken, string RefreshToken, DateTime expiresAt);
   
}
