namespace UnifiedAIChat.Api.Dtos.Messege
{
    public record SendMessegeRequest(Guid ChatId, string Messege, bool IsUser);
}
