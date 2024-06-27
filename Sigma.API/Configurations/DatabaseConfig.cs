using Microsoft.EntityFrameworkCore;
using Sigma.Database.Contexts;

namespace Sigma.API.Configurations;
public static class DatabaseConfig
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<ContextInitialiser>();
    }
}
