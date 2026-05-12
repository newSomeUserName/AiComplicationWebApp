using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Models.Chat
{
    public record DeleteChatCommand(Guid chatId, Guid userId);
}
