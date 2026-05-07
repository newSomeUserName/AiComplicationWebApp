using Microsoft.AspNetCore.Mvc;
using UnifiedAIChat.Api.Dtos.Auth;
using UnifiedAIChat.Application.Common.Models.Auth;
using UnifiedAIChat.Application.Services.Auth;

namespace UnifiedAIChat.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private const string ACCESS_TOKEN = "access_token"; 
        private const string REFRESH_TOKEN = "refresh_token";

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

            return Ok(tokensData); //TODO: return new type
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(LoginRequest request, CancellationToken ct)
        {
            LoginCommand command = new LoginCommand(request.Email, request.Password);

            //TODO: CHECK refresh token if exists

            LoginData tokensData = await _authService.LoginAsync(command, ct);
            AppendToknes(tokensData);
            return Ok(tokensData);  //TODO: return new type
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<string>> RefreshAsync(CancellationToken ct)
        {
            string rawRefreshToken = Request.Cookies["refresh_token"] ?? throw new Exception("Unable to read refresh token");
            LoginData tokensData = await _authService.RefreshAsync(rawRefreshToken);
            AppendToknes(tokensData);
            return Ok(tokensData);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync(CancellationToken ct)
        {
            string? rawRefreshToken = Request.Cookies["refresh_token"];

            if (rawRefreshToken is not null)
                await _authService.LogoutAsync(rawRefreshToken, ct);

            _deleteCookies(ACCESS_TOKEN);
            _deleteCookies(REFRESH_TOKEN);

            return NoContent();
        }
        private void _deleteCookies(string tokenType)
        {
            Response.Cookies.Delete(tokenType, new CookieOptions { 
                HttpOnly = true,
                Secure = true,
                Path = "/auth",
                SameSite = SameSiteMode.Strict, 
            });
        }
        private void AppendToknes(LoginData tokensData)
        {
            _appendTokens(tokensData.AccessToken, ACCESS_TOKEN);
            _appendTokens(tokensData.RefreshToken, REFRESH_TOKEN);
        }
        private void _appendTokens(string token, string tokenType)
        {
            Response.Cookies.Append(tokenType, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                Path = "/auth",
            });
        }
        
    }
}
