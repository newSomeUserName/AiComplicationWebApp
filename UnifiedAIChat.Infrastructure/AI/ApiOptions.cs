using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UnifiedAIChat.Infrastructure.AI
{
    public class ApiOptions
    {
        public const string SectionName = "ANTHROPIC_API_KEY";
        [Required, MinLength(10)]
        public string ApiKey { get; init; } = string.Empty;
    }
}
