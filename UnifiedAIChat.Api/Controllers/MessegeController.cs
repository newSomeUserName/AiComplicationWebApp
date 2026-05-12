using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UnifiedAIChat.Api.Dtos.Messege;
using UnifiedAIChat.Application.Common.Models.Messege;
using UnifiedAIChat.Application.Services.Messege;
using static System.Net.Mime.MediaTypeNames;

namespace UnifiedAIChat.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("chat")]
    public class MessegeController : ControllerBase
    {
        private readonly IMessegeService _messegeService;
        public MessegeController(IMessegeService messegeService)
        {
            _messegeService = messegeService;
        }
        [HttpPost("send")]
        public async Task SendAndRecieveMessegeAsync(SendMessegeRequest messegeRequest, CancellationToken ct)
        {
            var messegeCommand = new SendMessegeCommand(messegeRequest.ChatId, messegeRequest.Message, messegeRequest.IsUser);

            Response.ContentType = "text/event-stream";
            Response.Headers.Append("Cache-Control", "no-cache"); // dlya too chtobi kesh ne bufferizovalsya na proxy , inache otvet klienti priyudet polniy
            Response.Headers.Append("X-Accel-Buffering", "no");

            Guid chatId = await _messegeService.SendMessageAsync(messegeCommand, ct);

            await _getStreamingAsync(chatId,ct);


        }
        

        private async Task _getStreamingAsync(Guid chatId, CancellationToken ct)
        {
            await foreach (var item in _messegeService.GetReplyAsync(chatId))
            {
                if (string.IsNullOrEmpty(item)) continue;


                var json = JsonSerializer.Serialize(new { text = item });
                await Response.WriteAsync($"data: {json}\n\n", ct);

                await Response.Body.FlushAsync(ct);// CHTOBI DANNIE NE KOPILIS V BUFFERE A OTPRAVLYALIS SRAZUZ
            }
        }
    }
}
