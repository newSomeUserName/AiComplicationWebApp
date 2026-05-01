using UnifiedAIChat.Api;
using UnifiedAIChat.Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.InitializeInjections();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello");

app.Run();


