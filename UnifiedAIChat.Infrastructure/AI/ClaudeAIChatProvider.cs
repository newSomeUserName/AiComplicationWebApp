using Anthropic;
using Anthropic.Models.Messages;
using Anthropic.Services;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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



        public async IAsyncEnumerable<string> GenerateReplyAsync(IReadOnlyList<InputMessage> messages, [EnumeratorCancellation] CancellationToken ct)
        {

            List<MessageParam> mp = messages.Select(im => new MessageParam() { Content = im.Content, Role = im.Role.ToString().ToLower() }).ToList();


            MessageCreateParams parameters = new MessageCreateParams(){ MaxTokens = 1000, Messages = mp, Model = "claude-haiku-4-5"};




            IAsyncEnumerable<RawMessageStreamEvent> messageStream = _anthropicClient.Messages.CreateStreaming(parameters, ct);
            
            await foreach (RawMessageStreamEvent streamEvent in messageStream.WithCancellation(ct))
            {
                if (streamEvent.TryPickContentBlockDelta(out RawContentBlockDeltaEvent? delta))
                {
                    if (delta.Delta.TryPickText(out TextDelta? text))
                    {
                        string pickedText = Regex.Replace(text.Text, @"(?<!\n)\n(?!#|\n|-)", " ");

                        //yield return pickedText;
                        yield return text.Text;
                    }

                }
            }

        }
        


    }
}
