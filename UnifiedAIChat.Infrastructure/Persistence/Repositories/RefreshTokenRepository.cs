using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        //At first i will make a simple add then i will rewrite it to UoW
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByHashAsync(string hash)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(hash);

            return await _context.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(t => t.TokenHash == hash);

        }

        public async Task RevokeAllUserTokenAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(string hash)
        {
            throw new NotImplementedException();
        }
    }
}
