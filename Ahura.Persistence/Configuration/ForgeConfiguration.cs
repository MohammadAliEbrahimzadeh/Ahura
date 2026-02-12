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

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Name).HasMaxLength(100);

        builder.Property(x => x.ForgeSteps)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                  v => JsonSerializer.Deserialize<List<ForgeStep>>(v, (JsonSerializerOptions)null)
              );
    }
}
