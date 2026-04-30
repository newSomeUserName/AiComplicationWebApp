using System;
using System.Collections.Generic;
using System.Text;

namespace UnifiedAIChat.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
        public Role Role { get; set; }
    }
}
