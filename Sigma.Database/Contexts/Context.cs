using Microsoft.EntityFrameworkCore;
using Sigma.Database.Models;

namespace Sigma.Database.Contexts;

public sealed class Context : DbContext
{
    public Context(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Candidate> Candidates { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>()
            .HasIndex(c => c.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
