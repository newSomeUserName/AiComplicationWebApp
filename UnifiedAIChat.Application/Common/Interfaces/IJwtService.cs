using UnifiedAIChat.Application.Common.Models;

namespace UnifiedAIChat.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserTokenPayload userTokenPayload);
    }
}
