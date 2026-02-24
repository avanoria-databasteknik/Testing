namespace Infrastructure.Persistence.EfCore.Entities;

public sealed class InstructorRoleEntity
{
    public int Id { get; set; }
    public string RoleName { get; set; } = null!;

    public ICollection<InstructorEntity> Instructors { get; set; } = [];
}