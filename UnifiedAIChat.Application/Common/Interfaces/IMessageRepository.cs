using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Common.Interfaces
{
    public interface IMessageRepository
    {
        Task SendMessageAsync(Guid chatId, string message, MessageRole role);
    }
}
