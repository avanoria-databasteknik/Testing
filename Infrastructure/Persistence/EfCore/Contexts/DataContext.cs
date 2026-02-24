using Infrastructure.Persistence.EfCore.Configurations;
using Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EfCore.Contexts;

public sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<InstructorEntity> Instructors => Set<InstructorEntity>();
    public DbSet<InstructorRoleEntity> InstructorRoles => Set<InstructorRoleEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InstructorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InstructorRoleEntityConfiguration());
    }
}
