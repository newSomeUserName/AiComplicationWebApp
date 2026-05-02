using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<bool> IfEmailExistsAsync(string email, CancellationToken ct = default);
        Task AddUserAsync(User user, CancellationToken ct = default);
    }
}
