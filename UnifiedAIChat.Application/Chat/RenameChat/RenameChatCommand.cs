using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Application.Chat.RenameChat
{
    public record RenameChatCommand(Guid chatId,Guid userId,string newName);
}
