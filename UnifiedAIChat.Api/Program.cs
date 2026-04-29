using Microsoft.EntityFrameworkCore;
using UnifiedAIChat.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default_Connection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString, sqlOptions =>
                sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                ));

var app = builder.Build();



app.Run();


