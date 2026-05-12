using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces
{
    public interface IMessageRepository
    {
        Task SaveMessageAsync(Message messege, CancellationToken ct = default);
        Task<List<Message>> GetChatHistoryAsync(Guid chatId, CancellationToken ct = default);
    }
}
