using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces
{
    public interface IRefreshTokenRepository
    {
        Task<string> AddAsync(RefreshToken token, CancellationToken ct = default);
        Task<RefreshToken?> GetByHashAsync(string rawHash, CancellationToken ct = default);
        Task UpdateAsync(RefreshToken oldToken, string hash,CancellationToken ct = default);
        Task RevokeFamilyTokenAsync(Guid familyId, CancellationToken ct = default);
    }
}
