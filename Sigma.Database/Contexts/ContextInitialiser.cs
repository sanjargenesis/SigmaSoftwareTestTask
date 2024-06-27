using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sigma.Database.Contexts;

public sealed class ContextInitialiser
{
    private readonly Context _context;
    private readonly ILogger<ContextInitialiser> _logger;

    public ContextInitialiser(Context context, ILogger<ContextInitialiser> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                _logger.LogInformation("Applying migrations...");
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Migrations applied successfully.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw new Exception("An error occurred while initializing the database.", ex);
        }
    }
}