using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Auth.Register
{
    public record RegisterCommand(string Name, string Email, string Password);
}
