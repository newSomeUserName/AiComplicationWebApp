using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Models
{
    public record ChatCreateCommand(Guid UserId, string? Model);
    
}
