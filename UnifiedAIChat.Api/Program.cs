using Microsoft.AspNetCore.Mvc;
using UnifiedAIChat.Api;
using UnifiedAIChat.Infrastructure.AI;

var builder = WebApplication.CreateBuilder(args);

builder.InitializeInjections();

var app = builder.Build();


app.MapGet("/gpt", async ([FromServices]ClaudeAIChatProvider aIChatProvider) =>
{
    return await aIChatProvider.GenerateReplyAsync();
});


app.UseExceptionHandler("/error");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/error",( ) =>
{
    return "Jello";
});
app.MapGet("/home", () => "Hello home").RequireAuthorization();

app.Run();


