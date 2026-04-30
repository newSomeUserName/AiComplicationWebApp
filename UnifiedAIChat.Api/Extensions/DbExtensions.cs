using Microsoft.EntityFrameworkCore;
using UnifiedAIChat.Infrastructure.Persistence;

namespace UnifiedAIChat.Api.Extensions
{
    internal static class DbExtensions
    {
        public static IServiceCollection AddDbOptions<TDataBase>(this IServiceCollection collection, string connectionString) where TDataBase : DbContext
        {
            return collection.AddDbContext<TDataBase>(options => options.UseSqlServer(connectionString, sqlOptions =>
                sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                ));
        }
    }
}
