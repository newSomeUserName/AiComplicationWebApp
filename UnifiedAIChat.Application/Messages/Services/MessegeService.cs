using System.Runtime.CompilerServices;
using System.Text;
using UnifiedAIChat.Application.Common.Interfaces.AI;
using UnifiedAIChat.Application.Common.Interfaces.RepositoryInterfaces;
using UnifiedAIChat.Application.Messages.SendMessage;
using UnifiedAIChat.Domain.Entities;
using UnifiedAIChat.Domain.Entities.AI;
using Message = UnifiedAIChat.Domain.Entities.Message;

namespace UnifiedAIChat.Application.Messages.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IAIChatProvider _AIChatProvider;
        private readonly StringBuilder _sb;
        
        public MessageService(IMessageRepository messegeRepository, IAIChatProvider AIChatProvider)
        {

            _sb = new StringBuilder();
            _AIChatProvider = AIChatProvider;
            _messageRepository = messegeRepository;
        }

        public async Task<Guid> SendMessageAsync(SendMessegeCommand messegeCommand,CancellationToken ct)
        {
            Message message =
                new Message() { ChatId = messegeCommand.ChatId, Content = messegeCommand.Message, CreatedAt = DateTime.UtcNow, Role = MessageRole.User, Id = Guid.NewGuid() };
            await _messageRepository.SaveMessageAsync(message,ct);

            return message.Id;

        }
        public async IAsyncEnumerable<string> GetReplyAsync(Guid chatId, [EnumeratorCancellation] CancellationToken ct = default)
        {


            List<Message> messages = await _messageRepository.GetChatHistoryAsync(chatId, ct);
            List<InputMessage> newPromptCollection = messages.Select(m => new InputMessage { Content = m.Content, Role = m.Role }).ToList();

            await foreach (string chunkReply in _AIChatProvider.GenerateReplyAsync(newPromptCollection, ct))
            {
                _saveChunkReplyFromAssistant(chunkReply);
                yield return chunkReply;
            }


            string resultReply = _getFullReplyFromAssistant();
            Message replyMessage =
                new Message() { ChatId = chatId, Content = resultReply, CreatedAt = DateTime.UtcNow, Role = MessageRole.Assistant, Id = Guid.NewGuid() };
            
            await _messageRepository.SaveMessageAsync(replyMessage);

        }
        private void _saveChunkReplyFromAssistant(string chunkReply)=> _sb.Append(chunkReply);
        private string _getFullReplyFromAssistant() => _sb.ToString();
    }
}
