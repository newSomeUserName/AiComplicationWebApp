using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Infrastructure.Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;
        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendMessageAsync(Message message, CancellationToken ct)
        {
            await _context.Messages.AddAsync(message, ct);
            await _context.SaveChangesAsync(ct);
        }
    }
}
