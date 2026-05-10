using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Domain.Entities.AI
{
    public record ClaudeResponse(string Id, string Model, string Role, ContentBlock[] Content);
}
