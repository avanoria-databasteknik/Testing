using Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EfCore.Configurations;

internal sealed class InstructorEntityConfiguration : IEntityTypeConfiguration<InstructorEntity>
{
    public void Configure(EntityTypeBuilder<InstructorEntity> builder)
    {
        builder.ToTable("Instructors");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(450)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(320)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.RoleId)
            .IsRequired();

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Instructors)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Concurrency)
            .IsRowVersion();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ModifiedAt)
            .IsRequired();

    }
}
