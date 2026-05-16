using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UnifiedAIChat.Application.Chat.CreateChat;
using UnifiedAIChat.Application.Chat.DeleteChat;
using UnifiedAIChat.Application.Chat.RenameChat;
using UnifiedAIChat.Application.Chat.Services;
using UnifiedAIChat.Application.Common.Models;

namespace UnifiedAIChat.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {

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
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _chatService.DeleteChatAsync(new DeleteChatCommand(deleteRequest.ChatId,userId));

            return userId;
        }

        [HttpGet("chats")]
        public async Task<ActionResult<PagedResponse>> GetAllChatsAsync([FromQuery] string? cursor, [FromQuery] int limit = 20, CancellationToken ct = default)
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            await _chatService.GetAllChatsAsync(userId,cursor, limit, ct);

            return null;
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
