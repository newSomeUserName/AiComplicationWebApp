using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnifiedAIChat.Application.Messages.SendMessage;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Messages.Services
{
    public interface IMessageService
    {
        Task<Guid> SendMessageAsync(SendMessegeCommand messegeCommand, CancellationToken ct);
        IAsyncEnumerable<string> GetReplyAsync(Guid chatId, CancellationToken ct = default);
    }
}
