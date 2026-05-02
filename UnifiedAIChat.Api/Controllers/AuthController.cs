using Microsoft.AspNetCore.Mvc;
using UnifiedAIChat.Api.Dtos.Auth;
using UnifiedAIChat.Application.Common.Interfaces.Auth;
using UnifiedAIChat.Application.Common.Models.Auth;

namespace UnifiedAIChat.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("r")] //TODO : rename to /register
        public async Task<ActionResult<string>> RegisterAsync(RegisterRequest request, CancellationToken ct)
        {
            RegisterCommand command = new RegisterCommand(request.Name,request.Email, request.Password);
            string token = await _authService.RegisterAsync(command,ct);


            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(15)
            });

            return Ok(token);
        }
    }
}
