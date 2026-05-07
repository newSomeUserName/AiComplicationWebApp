using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Models
{
    public record RefreshTokenData(string RawToken, string Hash);
}
