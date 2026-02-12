using Ahura.Persistence.Context;

namespace Ahura.Infrastructure;

public class UnitOfWork : BaseUnitOfWork<AhuraDbContext>
{
    private readonly AhuraDbContext _context;

    public UnitOfWork(AhuraDbContext context) : base(context)
    {
        _context = context;
    }
}
