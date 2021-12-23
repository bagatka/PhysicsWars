using Microsoft.EntityFrameworkCore;
using PhysicsWars.Application.Common.DataAccess;
using PhysicsWars.Application.Features.Auth.Registration.Entities;
using PhysicsWars.Domain.Entities;

namespace PhysicsWars.Infrastructure.DataAccess;

public sealed class PhysicsWarsDbContext : DbContext, IPhysicsWarsDbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserForRegisterEntity> UsersToRegister { get; set; }

    public PhysicsWarsDbContext(DbContextOptions<PhysicsWarsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<UserEntity>(
                entity => entity
                    .ToTable("User")
                    .HasKey(u => u.Id)
            );

        modelBuilder
            .Entity<UserForRegisterEntity>(
                entity => entity
                    .ToTable("UserForRegister")
                    .HasKey(u => u.Id)
            );
    }
}