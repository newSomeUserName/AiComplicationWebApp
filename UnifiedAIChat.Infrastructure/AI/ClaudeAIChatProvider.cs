using Anthropic;
using Anthropic.Models.Messages;
using Anthropic.Services;
using UnifiedAIChat.Application.Common.Interfaces.AI;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using MessageCreateParams = Anthropic.Models.Messages.MessageCreateParams;

namespace UnifiedAIChat.Infrastructure.AI
{
    public class ClaudeAIChatProvider : IAIChatProvider
    {
        private readonly AnthropicClient _anthropicClient;
        //private readonly IMessageRepository _messageRepository;

        //_messageRepository = messageRepository;
        //    IMessageRepository messageRepository,
        public ClaudeAIChatProvider(AnthropicClient client)
        {
            
            _anthropicClient = client;
        }

        //string message, string model, CancellationToken ct
        public async Task<string> GenerateReplyAsync() 
        {
            //_anthropicClient.Messages.Create(new Anthropic.Models.Messages.MessageCreateParams() { Model = "claude-haiku-4-5", MaxTokens = 170, Messages =new MessageCreateParams },ct);


            MessageCreateParams parameters = new()
            {
                MaxTokens = 400,
                Messages = [new() { Role = "user", Content = "print(Hello World) C#", },],
                Model = "claude-haiku-4-5",
            };

            var messageGPT = await _anthropicClient.Messages.Create(parameters);

            foreach (var x in messageGPT.Content)
            {
                Console.WriteLine(x);
            }

            return messageGPT.ToString();
        }
    }
}
