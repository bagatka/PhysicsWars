using Microsoft.EntityFrameworkCore;

namespace PhysicsWars.Infrastructure.DataAccess;

public class PhysicsWarsDbContext : DbContext
{
    public PhysicsWarsDbContext(DbContextOptions<PhysicsWarsDbContext> options) : base(options)
    {
    }
}