using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Models.Chat;

namespace UnifiedAIChat.Application.Services.Chat
{
    public interface IChatService
    {
        Task<Guid> CreateChatAsync(CreateChatCommand chatCommand, CancellationToken ct = default);
        Task<Guid> DeleteChatAsync(DeleteChatCommand deleteChatCommand, CancellationToken ct = default);
        Task<Guid> RenameChatAsync(RenameChatCommand renameChatCommand, CancellationToken ct = default);
    }
}
