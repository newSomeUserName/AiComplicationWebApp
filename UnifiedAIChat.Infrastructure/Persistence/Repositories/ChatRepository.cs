using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Infrastructure.Persistence.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;
        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Chat chat, CancellationToken ct)
        {
            await _context.Chats.AddAsync(chat, ct);
            await _context.SaveChangesAsync(ct);
           
        }
    }
}
