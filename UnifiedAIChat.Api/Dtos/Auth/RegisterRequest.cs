using System.ComponentModel.DataAnnotations;

namespace UnifiedAIChat.Api.Dtos.Auth
{
    public record RegisterRequest([Required] string name, [Required, EmailAddress] string email, [Required, StringLength(64, MinimumLength = 10)] string password);
}
