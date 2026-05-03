using System.ComponentModel.DataAnnotations;

namespace UnifiedAIChat.Api.Dtos.Auth
{
    public record LoginRequest([Required, EmailAddress] string Email, [Required, StringLength(64, MinimumLength = 10)] string Password);
}
