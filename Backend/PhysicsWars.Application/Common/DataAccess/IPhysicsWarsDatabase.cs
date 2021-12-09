using System.Threading.Tasks;

namespace PhysicsWars.Application.Common.DataAccess;

public interface IPhysicsWarsDatabase
{
    // Repositories

    Task SaveAsync();
}