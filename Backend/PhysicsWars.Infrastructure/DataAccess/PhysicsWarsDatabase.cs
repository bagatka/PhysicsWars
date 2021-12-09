using PhysicsWars.Application.Common.DataAccess;

namespace PhysicsWars.Infrastructure.DataAccess;

public class PhysicsWarsDatabase : IPhysicsWarsDatabase
{
    private readonly PhysicsWarsDbContext _context;

    public PhysicsWarsDatabase(PhysicsWarsDbContext context)
    {
        _context = context;
    }

    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}