using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Common.Models.Chat;
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
        public async Task<Guid> CreateChatAsync(CreateChatCommand command, CancellationToken ct)
        {
            var chat = new Domain.Entities.Chat() { UserId = command.UserId, Model = command.Model };
            await _chatRepository.AddAsync(chat, ct);
            return chat.Id;
        }

        public async Task<Guid> DeleteChatAsync(DeleteChatCommand deleteChatCommand, CancellationToken ct = default)
        {
            return await _chatRepository.DeleteAsync(deleteChatCommand.chatId, deleteChatCommand.userId, ct);
        }
        public async Task<Guid> RenameChatAsync(Guid chatId, Guid userId, string newName, CancellationToken ct = default)
        {
            return await _chatRepository.RenameAsync(chatId, userId, newName, ct);
        }

        public async Task<Guid> RenameChatAsync(RenameChatCommand renameChatCommand, CancellationToken ct = default)
        {
            return await _chatRepository.RenameAsync(renameChatCommand.chatId, renameChatCommand.userId, renameChatCommand.newName, ct);
        }
    }
}
