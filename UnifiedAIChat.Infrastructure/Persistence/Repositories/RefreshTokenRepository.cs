using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        //private readonly UnitOfWork _unitOfWork; TODO: UoW

        //At first i will make a simple add then i will rewrite it to UoW
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddAsync(RefreshToken token, CancellationToken ct)
        {
            await _context.RefreshTokens.AddAsync(token, ct);
            await _context.SaveChangesAsync(ct);
            return token.TokenHash;
        }

        public async Task<RefreshToken?> GetByHashAsync(string refreshTokenHash, CancellationToken ct)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(refreshTokenHash);

            return await _context.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(t => t.TokenHash == refreshTokenHash, ct);

        }

        public async Task RevokeFamilyTokenAsync(Guid familyId, CancellationToken ct)
        {
            await _context.RefreshTokens.Where(rt => rt.FamilyId == familyId && rt.RevokedAt == null)
                .ExecuteUpdateAsync(sb => sb.SetProperty(t=>t.RevokedAt, DateTime.UtcNow), ct);
        }

        public async Task UpdateAsync(RefreshToken oldToken ,string newHash, CancellationToken ct)
        {
            oldToken.ReplacedByTokenHash = newHash;
            oldToken.RevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(ct);

        }
    }
}
