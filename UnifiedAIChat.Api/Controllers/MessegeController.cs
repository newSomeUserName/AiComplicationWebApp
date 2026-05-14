using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UnifiedAIChat.Application.Messages.SendMessage;
using UnifiedAIChat.Application.Messages.Services;

namespace UnifiedAIChat.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/messaege")]
    public class MessegeController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessegeController(IMessageService messegeService)
        {
            _messageService = messegeService;
        }
        [HttpPost("send")]
        public async Task SendAndRecieveMessegeAsync(SendMessegeRequest messegeRequest, CancellationToken ct)
        {
            var messegeCommand = new SendMessegeCommand(messegeRequest.ChatId, messegeRequest.Message, messegeRequest.IsUser);

            Response.ContentType = "text/event-stream";
            Response.Headers.Append("Cache-Control", "no-cache"); // dlya too chtobi kesh ne bufferizovalsya na proxy , inache otvet klienti priyudet polniy
            Response.Headers.Append("X-Accel-Buffering", "no");

            Guid chatId = await _messageService.SendMessageAsync(messegeCommand, ct);

            await _getStreamingAsync(chatId,ct);


        }
        

        private async Task _getStreamingAsync(Guid chatId, CancellationToken ct)
        {
            await foreach (var item in _messageService.GetReplyAsync(chatId))
            {
                if (string.IsNullOrEmpty(item)) continue;


                var json = JsonSerializer.Serialize(new { text = item });
                await Response.WriteAsync($"data: {json}\n\n", ct);

                await Response.Body.FlushAsync(ct);// CHTOBI DANNIE NE KOPILIS V BUFFERE A OTPRAVLYALIS SRAZUZ
            }
        }
    }
}
