using System.ComponentModel.DataAnnotations;

namespace UnifiedAIChat.Application.Auth.Register
{
    public record RegisterRequest([Required] string Name, [Required, EmailAddress] string Email, [Required, StringLength(64, MinimumLength = 10)] string Password);
}
