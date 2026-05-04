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

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterAsync(RegisterRequest request, CancellationToken ct)
        {
            RegisterCommand command = new RegisterCommand(request.Name,request.Email, request.Password);
            LoginData tokensData = await _authService.RegisterAsync(command,ct);


            AppendToknes(tokensData);

            return Ok(tokensData);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(LoginRequest request, CancellationToken ct)
        {
            LoginCommand command = new LoginCommand(request.Email, request.Password);
            LoginData tokensData = await _authService.LoginAsync(command, ct);
            AppendToknes(tokensData);

            return Ok(tokensData);
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<string>> RefreshAsync(CancellationToken ct)
        {
            string rawRefreshToken = Request.Cookies["refresh_token"] ?? throw new Exception("Unable to read refresh token");

            LoginData tokensData = await _authService.RefreshAsync(rawRefreshToken);

            AppendToknes(tokensData);

            return Ok(tokensData);
        }

        private void AppendToknes(LoginData tokensData)
        {
            Response.Cookies.Append("access_token", tokensData.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(15)
            });

            Response.Cookies.Append("refresh_token", tokensData.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(35)
            });
        }
    }
}
