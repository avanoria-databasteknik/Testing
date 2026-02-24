namespace Infrastructure.Persistence.EfCore.Entities;

public sealed class InstructorEntity
{
    public string Id { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public int RoleId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public byte[] Concurrency { get; set; } = null!;

    public InstructorRoleEntity Role { get; set; } = null!;

}

public sealed class InstructorRoleEntity
{
    public int Id { get; set; }
    public string RoleName { get; set; } = null!;

    public ICollection<InstructorEntity> Instructors { get; set; } = [];
}