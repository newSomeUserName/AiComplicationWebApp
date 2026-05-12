using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UnifiedAIChat.Api.Dtos.Chat.Requests;
using UnifiedAIChat.Application.Common.Models;
using UnifiedAIChat.Application.Common.Models.Chat;
using UnifiedAIChat.Application.Services.Chat;

namespace UnifiedAIChat.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("chat")]
    public class ChatController : ControllerBase
    {
        //TODO: registrate IChatService AND IChatRepository

        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateChat([FromBody]CreateChatRequest chatRequest, CancellationToken ct = default)
        {

            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
            var chatId = await _chatService.CreateChatAsync(new CreateChatCommand(userId, chatRequest.Model), ct);
            return Ok(chatId);
        }
        [HttpDelete("delete")]
        public async Task<Guid> DeleteChatAsync(DeleteChatRequest deleteRequest, CancellationToken ct = default)
        {
            Guid userId = Guid.Parse(User.FindFirst("Id")!.Value);
            await _chatService.DeleteChatAsync(new DeleteChatCommand(deleteRequest.ChatId,userId));

            return userId;
        }
        [HttpPut("rename")]
        public async Task<Guid> RenameChatAsync(RenameChatRequest renameRequest, CancellationToken ct = default)
        {
            Guid userId = Guid.Parse(User.FindFirst("Id")!.Value);

            Guid chatId =await _chatService.RenameChatAsync(new RenameChatCommand(renameRequest.ChatId,userId,renameRequest.NewName));

            return chatId;
        }
    }
}
