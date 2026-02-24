using Domain.Instructors;

namespace Tests.Unit.Aggregates;

public class InstructorRoleTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateInstance()
    {
        var role = InstructorRole.Create(1, "Senior Instructor");

        Assert.Equal(1, role.Id);
        Assert.Equal("Senior Instructor", role.RoleName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Create_WithInvalidId_ShouldThrowInvalidOperationException(int invalidId)
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            InstructorRole.Create(invalidId, "Instructor"));

        Assert.Equal("Id is required", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithInvalidRoleName_ShouldThrowInvalidOperationException(string? invalidName)
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            InstructorRole.Create(1, invalidName!));

        Assert.Equal("Role name is required", ex.Message);
    }

    [Fact]
    public void Create_WithBoundaryId_ShouldSucceed()
    {
        var role = InstructorRole.Create(1, "Assistant");

        Assert.Equal(1, role.Id);
        Assert.Equal("Assistant", role.RoleName);
    }
}
