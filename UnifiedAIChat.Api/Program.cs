using UnifiedAIChat.Api;

var builder = WebApplication.CreateBuilder(args);

builder.InitializeInjections();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapGet("/", async (HttpContext ctx) =>
//{
//    ctx.Response.Headers.ContentType = "text/html";
//    await ctx.Response.SendFileAsync("wwwroot/index.html");
//});
//app.MapPost("/reg", ([FromServices] UnifiedAIChat.Application.Common.Interfaces.IJwtService s, HttpContext ctx) =>
//{
//    var t = s.GenerateToken(new UnifiedAIChat.Application.Common.Models.UserTokenPayload { Email = "aaaa@gmail.com", Id = "gera", Name = "kudah" });

//    ctx.Response.Cookies.Append("access_token", t, new CookieOptions
//    {
//        HttpOnly = true,
//        Secure = true,
//        SameSite = SameSiteMode.Strict,
//        Expires = DateTimeOffset.UtcNow.AddMinutes(15)
//    });

//    return Results.Ok(t);
//});

app.MapControllers();

app.MapGet("/home", () => "Hello home").RequireAuthorization();

app.Run();


