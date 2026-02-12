using Ahura.Persistence.Enums;


namespace Ahura.Persistence.Entities;

public class ForgeStep
{
    public int Order { get; set; }

    public ActionTypeEnum ActionType { get; set; }

    public string? Configuration { get; set; }
}
