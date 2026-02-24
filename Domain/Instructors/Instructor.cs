using Domain.Common.ValueObjects;

namespace Domain.Instructors;

public sealed class Instructor
{
    private Instructor(string id, string firstName, string lastName, string email, InstructorRole role)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new InvalidOperationException("Id is required");

        Id = id;

        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidOperationException("First name is required");

        FirstName = firstName;

        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidOperationException("Last name is required");

        LastName = lastName;

        Email = new EmailAddress(email);

        if (role is null)
            throw new InvalidOperationException("Instructor role is required");

        Role = role;
    }

    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public EmailAddress Email { get; set; }

    public InstructorRole Role { get; set; }

    public static Instructor Create(string id, string firstName, string lastName, string email, InstructorRole role)
        => new(id, firstName, lastName, email, role);

}
