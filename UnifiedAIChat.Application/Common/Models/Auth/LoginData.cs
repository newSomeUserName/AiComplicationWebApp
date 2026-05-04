using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Models.Auth
{
    public record LoginData(string AccessToken, string RefreshToken, DateTime expiresAt);
   
}
