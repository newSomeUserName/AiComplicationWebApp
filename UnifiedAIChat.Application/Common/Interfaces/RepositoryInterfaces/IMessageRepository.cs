using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces
{
    public interface IMessageRepository
    {
        Task SendMessageAsync(Message messege, CancellationToken ct = default);
    }
}
