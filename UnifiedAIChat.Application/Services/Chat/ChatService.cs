using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Common.Models;
using UnifiedAIChat.Domain.Entities;



namespace UnifiedAIChat.Application.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        


        public ChatService(IChatRepository chatRepository, IMessageRepository messageRepository)
        {
            _chatRepository = chatRepository;
        }
        public async Task<Guid> CreateChatAsync(ChatCreateCommand command, CancellationToken ct)
        {
            var chat = new Domain.Entities.Chat() { UserId = command.UserId, Model = command.Model };
            await _chatRepository.AddAsync(chat, ct);
            return chat.Id;
        }
        


    }
}
