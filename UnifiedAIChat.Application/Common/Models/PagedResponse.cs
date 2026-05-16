using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Chat.GetChats;

namespace UnifiedAIChat.Application.Common.Models
{
    public record PagedResponse(IReadOnlyList<ChatResponse> Chats, int TotalCount, bool HasNextPage, string? NextCursor);
    
}
