using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Domain.Entities;
using UnifiedAIChat.Domain.Entities.AI;

namespace UnifiedAIChat.Application.Common.Interfaces.AI
{
    public interface IAIChatProvider
    {
        Task<string> GenerateReplyAsync(IReadOnlyList<InputMessage> message, CancellationToken ct = default);
    }
}
