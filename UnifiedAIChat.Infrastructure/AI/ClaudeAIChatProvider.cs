using Anthropic;
using Anthropic.Models.Messages;
using Anthropic.Services;
using System.Collections.Immutable;
using System.Text.Json;
using UnifiedAIChat.Application.Common.Interfaces.AI;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Domain.Entities.AI;
using UnifiedAIChat.Infrastructure.Persistence.Repositories;
using MessageCreateParams = Anthropic.Models.Messages.MessageCreateParams;

namespace UnifiedAIChat.Infrastructure.AI
{
    public class ClaudeAIChatProvider : IAIChatProvider
    {
        private readonly AnthropicClient _anthropicClient;

        public ClaudeAIChatProvider(AnthropicClient client)
        {
            _anthropicClient = client;
        }

        
        public async Task<string> GenerateReplyAsync(IReadOnlyList<InputMessage> messages, CancellationToken ct) 
        {

            List<MessageParam> mp = messages.Select(im=> new MessageParam() { Content = im.Content, Role = im.Role.ToString().ToLower()}).ToList();


            MessageCreateParams parameters = new()
            {
                MaxTokens = 1000,
                Messages = mp,
                Model = "claude-haiku-4-5",
            };

            var messageGPT = await _anthropicClient.Messages.Create(parameters);

            return messageGPT.ToString();
        }
    }
}
