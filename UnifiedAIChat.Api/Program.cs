using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UnifiedAIChat.Api.AppConfiguration.Models;
using UnifiedAIChat.Api.Extensions;
using UnifiedAIChat.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;
builder.Services.AddJwtAuthentication(jwtOptions);

var connectionString = builder.Configuration.GetConnectionString("Default_Connection")!;
builder.Services.AddDbOptions<AppDbContext>(connectionString);

builder.Services.AddAuthorization();

var app = builder.Build();


app.UseAuthentication();

app.Run();


