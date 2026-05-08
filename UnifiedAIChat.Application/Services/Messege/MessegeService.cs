using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces.AI;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Common.Models.Messege;
using UnifiedAIChat.Domain.Entities;
using UnifiedAIChat.Domain.Entities.AI;

namespace UnifiedAIChat.Application.Services.Messege
{
    public class MessegeService : IMessegeService
    {
        private readonly IMessageRepository _messegeRepository;
        private readonly IAIChatProvider _AIChatProvider;
        public MessegeService(IMessageRepository messegeRepository, IAIChatProvider AIChatProvider)
        {
            _AIChatProvider = AIChatProvider;
            _messegeRepository = messegeRepository;
        }
        public async Task<string> SendMessageAsync(SendMessegeCommand messegeCommand, CancellationToken ct)
        {

            List<Message> messages = await _messegeRepository.GetChatHistoryAsync(messegeCommand.ChatId, ct);
            var newPromptCollection = messages.Select(m => new InputMessage { Content = m.Content, Role = m.Role }).ToList();
            newPromptCollection.Add(new InputMessage() { Content = messegeCommand.Message, Role = messegeCommand.IsUser ? MessageRole.User : MessageRole.Assistant });
            var result = await _AIChatProvider.GenerateReplyAsync(newPromptCollection, ct);

            await _messegeRepository.SendMessageAsync(new Message() { ChatId = messegeCommand.ChatId, Content = messegeCommand.Message, CreatedAt = DateTime.UtcNow , Role = MessageRole.User, Id = Guid.NewGuid()},ct);//rename to SaveMessageAsync()
            await _messegeRepository.SendMessageAsync(new Message() { ChatId = messegeCommand.ChatId, Content = result, CreatedAt = DateTime.UtcNow, Role = MessageRole.Assistant, Id = Guid.NewGuid() }, ct);//rename to SaveMessageAsync()

            return result;
        }
    }
}
