namespace Domain.Instructors;

public sealed class InstructorRole
{
    private InstructorRole(int id, string roleName)
    {
        if (id <= 0)
            throw new InvalidOperationException("Id is required");

        Id = id;

        if (string.IsNullOrWhiteSpace(roleName))
            throw new InvalidOperationException("Role name is required");

        RoleName = roleName;
    }

    private InstructorRole(string roleName)
    {

        if (string.IsNullOrWhiteSpace(roleName))
            throw new InvalidOperationException("Role name is required");

        RoleName = roleName;
    }

    public int Id { get; private set; }
    public string RoleName { get; private set; }

    public static InstructorRole Create(int id, string roleName) 
        => new(id, roleName);

    public static InstructorRole Create(string roleName)
    => new(roleName);

}
