using Ahura.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Ahura.Persistence.Configuration;

public class ForgeConfiguration : IEntityTypeConfiguration<Forge>
{
    public void Configure(EntityTypeBuilder<Forge> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasQueryFilter(x => x.IsDeleted == false);

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Name).HasMaxLength(100);

        builder.Property(x => x.ForgeSteps)
                .HasColumnType("json");

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Forges)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
