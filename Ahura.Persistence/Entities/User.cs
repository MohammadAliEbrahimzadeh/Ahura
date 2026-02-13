using Radenoor.Utilities;

namespace Ahura.Persistence.Entities;

public partial class User : BaseEntity<long>
{
    public string? Username { get; set; }

    public string? PhoneNumber { get; set; }
}

public partial class User
{
    public ICollection<Forge>? Forges { get; set; } = new HashSet<Forge>();
}
