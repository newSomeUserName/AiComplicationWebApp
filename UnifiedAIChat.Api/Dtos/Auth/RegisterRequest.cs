using System.ComponentModel.DataAnnotations;

namespace UnifiedAIChat.Api.Dtos.Auth
{
    public record RegisterRequest([Required] string Name, [Required, EmailAddress] string Email, [Required, StringLength(64, MinimumLength = 10)] string Password);
}
