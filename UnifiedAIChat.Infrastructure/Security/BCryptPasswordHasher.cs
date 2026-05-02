using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces;
using BC = BCrypt.Net.BCrypt;


namespace UnifiedAIChat.Infrastructure.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        private const int WORK_FACTOR = 12;
        
        public string HashPassword(string password)
        {
            return BC.HashPassword(password, WORK_FACTOR);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BC.Verify(password, hash);
        }
    }
}
