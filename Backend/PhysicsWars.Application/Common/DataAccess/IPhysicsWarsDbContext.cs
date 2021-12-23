using Microsoft.EntityFrameworkCore;
using PhysicsWars.Application.Features.Auth.Registration.Entities;
using PhysicsWars.Domain.Entities;

namespace PhysicsWars.Application.Common.DataAccess;

public interface IPhysicsWarsDbContext
{
    DbSet<UserEntity> Users { get; set; }
    DbSet<UserForRegisterEntity> UsersToRegister { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}