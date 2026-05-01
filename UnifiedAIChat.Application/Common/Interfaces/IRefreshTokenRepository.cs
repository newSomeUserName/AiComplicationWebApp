using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Common.Interfaces
{
    public interface IRefreshTokenRepository
    {
        void AddAsync(RefreshToken token);
        string GetByHashAsync(string hash);
        void RevokeAllUserTokenAsync(Guid userId);
    }
}
