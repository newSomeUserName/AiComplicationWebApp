namespace UnifiedAIChat.Api.AppConfiguration.Models
{
    internal class JwtOptions
    {
        public const string SectionName = "Jwt";
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string ExpirationMinutes { get; set; } = string.Empty;
    }
}
