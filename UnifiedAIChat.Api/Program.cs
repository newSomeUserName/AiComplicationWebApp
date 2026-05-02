using UnifiedAIChat.Api;

var builder = WebApplication.CreateBuilder(args);

builder.InitializeInjections();

var app = builder.Build();
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


