using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Common.Models.Chat
{
    public record RenameChatCommand(Guid chatId,Guid userId,string newName);
}
