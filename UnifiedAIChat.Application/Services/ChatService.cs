using UnifiedAIChat.Application.Common.Interfaces;
using UnifiedAIChat.Application.Common.Interfaces.Chat;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Common.Models;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;

        
        public ChatService(IChatRepository chatRepository, IMessageRepository messageRepository)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }
        public async Task<Guid> CreateChatAsync(ChatCreateCommand command,CancellationToken ct)
        {


            var chat = new Chat() { UserId = command.UserId, Model = command.Model };
            await _chatRepository.AddAsync(chat, ct);

            return chat.Id;
        }

        
    }
}
