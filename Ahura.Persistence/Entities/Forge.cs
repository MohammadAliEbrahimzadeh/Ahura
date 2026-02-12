using Radenoor.Utilities;

namespace Ahura.Persistence.Entities;

public class Forge : BaseEntity<long>
{
    public string? Name { get; set; }

    public ICollection<ForgeStep>? ForgeSteps { get; set; } = new HashSet<ForgeStep>();
}
