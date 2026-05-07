using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Common.Models.Messege;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Services.Messege
{
    public class MessegeService : IMessegeService
    {
        private readonly IMessageRepository _messegeRepository;
        public MessegeService(IMessageRepository messegeRepository)
        {
            _messegeRepository = messegeRepository;
        }
        public async Task<Guid> SendMessageAsync(SendMessegeCommand messegeCommand, CancellationToken ct)
        {
            MessageRole mr = messegeCommand.IsUser ? MessageRole.User : MessageRole.System;

            Message messege = new Message() { Id = Guid.NewGuid(),ChatId = messegeCommand.ChatId, Content = messegeCommand.Message, Role = mr , CreatedAt = DateTime.UtcNow};
            await _messegeRepository.SendMessageAsync(messege, ct);

            return messege.Id;
        }
    }
}
