namespace UnifiedAIChat.Application.Messages.SendMessage
{
    public record SendMessegeRequest(Guid ChatId, string Message, bool IsUser);
}
