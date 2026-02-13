using Radenoor.Utilities;

namespace Ahura.Persistence.Entities;

public partial class Forge : BaseEntity<long>
{
    public string? Name { get; set; }

    public string? ForgeSteps { get; set; }

    public long UserId { get; set; }
}

public partial class Forge
{
    public User? User { get; set; }
}
