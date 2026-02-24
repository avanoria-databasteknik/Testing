using Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EfCore.Configurations;

internal sealed class InstructorRoleEntityConfiguration : IEntityTypeConfiguration<InstructorRoleEntity>
{
    public void Configure(EntityTypeBuilder<InstructorRoleEntity> builder)
    {
        builder.ToTable("InstructorRoles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.RoleName)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.RoleName)
            .IsUnique();

        builder.Navigation(x => x.Instructors)
            .AutoInclude(false);
    }
}