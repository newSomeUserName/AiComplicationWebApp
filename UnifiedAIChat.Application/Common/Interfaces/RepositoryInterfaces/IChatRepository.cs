using ChatClass = UnifiedAIChat.Domain.Entities.Chat;

namespace UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces
{
    public interface IChatRepository
    {
        Task AddAsync(ChatClass chats, CancellationToken ct = default);
    }
}
