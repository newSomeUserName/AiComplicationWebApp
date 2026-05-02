using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Models.Auth
{
    public record RegisterCommand(string name, string email, string password);
}
