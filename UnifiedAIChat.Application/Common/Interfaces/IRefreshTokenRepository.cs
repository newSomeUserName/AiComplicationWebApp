using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Common.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByHashAsync(string hash);
        Task UpdateAsync(string hash);
        Task RevokeAllUserTokenAsync(Guid userId);
    }
}
