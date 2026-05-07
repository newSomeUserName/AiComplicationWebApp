using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Models;

namespace UnifiedAIChat.Application.Common.Interfaces.Chat
{
    public interface IChatService
    {
        Task<Guid> CreateChatAsync(ChatCreateCommand chatCommand, CancellationToken ct = default);
    }
}
