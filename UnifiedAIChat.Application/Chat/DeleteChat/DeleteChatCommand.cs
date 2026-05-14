using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Chat.DeleteChat
{
    public record DeleteChatCommand(Guid chatId, Guid userId);
}
