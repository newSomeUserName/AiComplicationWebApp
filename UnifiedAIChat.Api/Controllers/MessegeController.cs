using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnifiedAIChat.Api.Dtos.Messege;
using UnifiedAIChat.Application.Common.Models.Messege;
using UnifiedAIChat.Application.Services.Messege;

namespace UnifiedAIChat.Api.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("messege")]
    public class MessegeController : ControllerBase
    {
        private readonly IMessegeService _messegeService;
        public MessegeController(IMessegeService messegeService)
        {
            _messegeService = messegeService;
        }
        [HttpPost("send")]
        public async Task<ActionResult<string>> SendMessegeAsync(SendMessegeRequest messegeRequest, CancellationToken ct)
        {
            var messegeCommand = new SendMessegeCommand(messegeRequest.ChatId, messegeRequest.Message, messegeRequest.IsUser);
            var replyMessage = await _messegeService.SendMessageAsync(messegeCommand, ct);
            return replyMessage.ToString();
        }
    }
}
