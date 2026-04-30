using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UnifiedAIChat.Api.AppConfiguration.Models;
using UnifiedAIChat.Api.Extensions;
using UnifiedAIChat.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default_Connection")!;
builder.Services.AddDbOptions<AppDbContext>(connectionString);

var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;
builder.Services.AddJwtAuthentication(jwtOptions);

builder.Services.AddAuthorization();

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello");
app.MapGet("/login", () => "login get");


app.MapPost("/register", (string email, string password) => { 

});



app.MapGet("/auth", () => "Hello Auth").RequireAuthorization();


app.Run();


