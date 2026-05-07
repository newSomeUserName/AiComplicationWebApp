using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces;
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

        public async Task SendMessageAsync(Guid chatId, string message, MessageRole role)
        {
            await _context.Messages.AddAsync(new Message() { ChatId = chatId, Content = message, Role = role });
        }
    }
}
