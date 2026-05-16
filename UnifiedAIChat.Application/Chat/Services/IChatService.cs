using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Chat.CreateChat;
using UnifiedAIChat.Application.Chat.DeleteChat;
using UnifiedAIChat.Application.Chat.RenameChat;
using UnifiedAIChat.Application.Common.Models;

namespace UnifiedAIChat.Application.Chat.Services
{
    public interface IChatService
    {
        Task<Guid> CreateChatAsync(CreateChatCommand chatCommand, CancellationToken ct = default);
        Task<Guid> DeleteChatAsync(DeleteChatCommand deleteChatCommand, CancellationToken ct = default);
        Task<Guid> RenameChatAsync(RenameChatCommand renameChatCommand, CancellationToken ct = default);
        Task<PagedResponse> GetAllChatsAsync(Guid userId,string? cursor,int limit,CancellationToken ct = default);
    }
}
