using Domain.Instructors;

namespace Tests.Unit.Aggregates;

public class InstructorTests
{
    private static InstructorRole ValidRole()
    => InstructorRole.Create(1, "Senior Instructor");

    [Fact]
    public void Create_WithValidData_ShouldCreateInstructor()
    {
        var instructor = Instructor.Create(
            "abc123",
            "Hans",
            "Mattin-Lassei",
            "hans@example.com",
            ValidRole());

        Assert.Equal("abc123", instructor.Id);
        Assert.Equal("Hans", instructor.FirstName);
        Assert.Equal("Mattin-Lassei", instructor.LastName);
        Assert.Equal("hans@example.com", instructor.Email.Value);
        Assert.Equal(1, instructor.Role.Id);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidId_ShouldThrow(string? invalidId)
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            Instructor.Create(invalidId!, "Hans", "Test", "test@test.com", ValidRole()));

        Assert.Equal("Id is required", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidFirstName_ShouldThrow(string? invalidFirstName)
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            Instructor.Create("1", invalidFirstName!, "Test", "test@test.com", ValidRole()));

        Assert.Equal("First name is required", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidLastName_ShouldThrow(string? invalidLastName)
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            Instructor.Create("1", "Hans", invalidLastName!, "test@test.com", ValidRole()));

        Assert.Equal("Last name is required", ex.Message);
    }

    [Fact]
    public void Create_WithInvalidEmail_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
            Instructor.Create("1", "Hans", "Test", "invalid-email", ValidRole()));
    }

    [Fact]
    public void Create_WithNullRole_ShouldThrow()
    {
        Assert.Throws<InvalidOperationException>(() =>
            Instructor.Create("1", "Hans", "Test", "test@test.com", null!));
    }
}
