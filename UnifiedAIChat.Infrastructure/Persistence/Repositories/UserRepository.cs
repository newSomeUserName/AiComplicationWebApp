using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }
        public Task AddUserAsync(User user, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IfEmailExistsAsync(string email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
