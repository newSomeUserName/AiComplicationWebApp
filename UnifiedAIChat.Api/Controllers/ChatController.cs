using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UnifiedAIChat.Api.Dtos.Chat;
using UnifiedAIChat.Application.Common.Models;
using UnifiedAIChat.Application.Services.Chat;

namespace UnifiedAIChat.Api.Controllers
{
    [ApiController]
    [Route("chat")]
    public class ChatController : ControllerBase
    {
        //TODO: registrate IChatService AND IChatRepository

        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateChat([FromBody]CreateChatRequest chatRequest, CancellationToken ct)
        {

            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
            var chatId = await _chatService.CreateChatAsync(new ChatCreateCommand(userId, chatRequest.Model), ct);
            return Ok(chatId);
        }
    }
}
