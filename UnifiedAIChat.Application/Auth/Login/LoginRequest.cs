using System.ComponentModel.DataAnnotations;

namespace UnifiedAIChat.Application.Auth.Login
{
    public record LoginRequest([Required, EmailAddress] string Email, [Required, StringLength(64, MinimumLength = 10)] string Password);
}
