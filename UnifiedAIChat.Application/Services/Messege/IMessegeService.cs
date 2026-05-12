using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnifiedAIChat.Application.Common.Models.Messege;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Services.Messege
{
    public interface IMessegeService
    {
        Task<Guid> SendMessageAsync(SendMessegeCommand messegeCommand, CancellationToken ct);
        IAsyncEnumerable<string> GetReplyAsync(Guid chatId, [EnumeratorCancellation] CancellationToken ct = default);
    }
}
