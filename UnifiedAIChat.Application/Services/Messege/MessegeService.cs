using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces.AI;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Common.Models.Messege;
using UnifiedAIChat.Domain.Entities;
using UnifiedAIChat.Domain.Entities.AI;

namespace UnifiedAIChat.Application.Services.Messege
{
    public class MessegeService : IMessegeService
    {
        private readonly IMessageRepository _messegeRepository;
        private readonly IAIChatProvider _AIChatProvider;


        private readonly StringBuilder _sb;
        public MessegeService(IMessageRepository messegeRepository, IAIChatProvider AIChatProvider)
        {

            _sb = new StringBuilder();
            _AIChatProvider = AIChatProvider;
            _messegeRepository = messegeRepository;
        }
        public async IAsyncEnumerable<string> SendMessageAsync(SendMessegeCommand messegeCommand, [EnumeratorCancellation] CancellationToken ct)
        {


            await _messegeRepository.SendMessageAsync(new Message() { ChatId = messegeCommand.ChatId, Content = messegeCommand.Message, CreatedAt = DateTime.UtcNow, Role = MessageRole.User, Id = Guid.NewGuid() }, ct);//rename to SaveMessageAsync()

            List<Message> messages = await _messegeRepository.GetChatHistoryAsync(messegeCommand.ChatId, ct);
            List<InputMessage> newPromptCollection = messages.Select(m => new InputMessage { Content = m.Content, Role = m.Role }).ToList();




            await foreach (string chunkReply in _AIChatProvider.GenerateReplyAsync(newPromptCollection, ct))
            {
                _saveChunkReplyFromAssistant(chunkReply);
                yield return chunkReply;
            }


            string resultReply = _getFullReplyFromAssistant();

            await _messegeRepository.SendMessageAsync(new Message() { ChatId = messegeCommand.ChatId, Content = resultReply, CreatedAt = DateTime.UtcNow, Role = MessageRole.Assistant, Id = Guid.NewGuid() });//rename to SaveMessageAsync()

        }
        private void _saveChunkReplyFromAssistant(string chunkReply)=> _sb.Append(chunkReply);
        private string _getFullReplyFromAssistant() => _sb.ToString();
    }
}
