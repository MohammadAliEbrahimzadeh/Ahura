using Ahura.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ahura.Persistence.Context;

public class AhuraDbContext : DbContext
{
    public AhuraDbContext(DbContextOptions<AhuraDbContext> options) : base(options)
    { 
    }

    public DbSet<Forge>? Forges { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AhuraDbContext).Assembly);
    }
}
