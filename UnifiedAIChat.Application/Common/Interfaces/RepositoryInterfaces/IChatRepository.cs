using ChatClass = UnifiedAIChat.Domain.Entities.Chat;

namespace UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces
{
    public interface IChatRepository
    {
        Task AddAsync(ChatClass chats, CancellationToken ct = default);
        Task<Guid> RenameAsync(Guid chatId, Guid userId, string newName, CancellationToken ct = default);
        Task<Guid> DeleteAsync(Guid chatId,Guid userId, CancellationToken ct = default);
    }
}
