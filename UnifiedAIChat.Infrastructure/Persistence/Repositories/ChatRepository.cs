using Microsoft.EntityFrameworkCore;
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
        public async Task<Guid> DeleteAsync(Guid chatId, Guid userId, CancellationToken ct = default)
        {
            var chat  = await _context.Chats.FirstAsync(c=> c.Id == chatId && c.UserId == userId) ?? throw new Exception("Not found");

            chat.IsDeleted = true;
            chat.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync(ct);

            return chat.Id;

        }

        

        public async Task<Guid> RenameAsync(Guid chatId, Guid userId, string newName, CancellationToken ct = default)
        {
            var chat = await _context.Chats.FirstAsync(c => c.Id == chatId && c.UserId == userId) ?? throw new Exception("Not found");

            chat.ChatName = newName;

            await _context.SaveChangesAsync(ct);

            return chat.Id;
        }
    }
}
