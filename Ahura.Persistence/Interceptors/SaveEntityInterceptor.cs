using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Radenoor.Utilities;

namespace Ahura.Persistence.Interceptors;

public class SaveEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        var entries = dbContext.ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            // 1. Handle Added Entities (INSERT)
            if (entry.State == EntityState.Added)
            {
                // If the entity inherits from BaseEntity
                if (entry.Entity is BaseEntity<long> entity)
                {
                    // Set CreatedAt (only if not manually set)
                    if (entity.CreatedAt == default)
                    {
                        entity.CreatedAt = DateTime.UtcNow;
                    }

                    // Set UpdatedAt
                    entity.UpdatedAt = DateTime.UtcNow;
                }
            }
            // 2. Handle Modified Entities (UPDATE)
            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is BaseEntity<long> entity)
                {
                    // Always update UpdatedAt
                    entity.UpdatedAt = DateTime.UtcNow;

                    // Prevent overwriting CreatedAt (Security/Logic fix)
                    entry.Property(nameof(BaseEntity<long>.CreatedAt)).IsModified = false;

                }
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
