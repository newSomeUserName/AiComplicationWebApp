using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Models
{
    public class RefreshTokenData
    {
        public string RawToken { get; set; }
        public string Hash { get; set; }
    }
}
