namespace UnifiedAIChat.Application.Chat.GetChats
{
    public record ChatResponse(Guid Id,string ChatName,DateTime LastMessageAt, int MessageCount);
}
