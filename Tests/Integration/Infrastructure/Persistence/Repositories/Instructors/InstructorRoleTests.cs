using Domain.Instructors;
using Infrastructure.Persistence.EfCore.Contexts;
using Infrastructure.Persistence.EfCore.Repositories.Instructors;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration.Infrastructure.Persistence.Repositories.Instructors;

public class InstructorRoleTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateInstructorRole_ReturnTrue()
    {
        CancellationToken ct = CancellationToken.None;

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer("Data Source=localhost;Initial Catalog=CourseOnline;Persist Security Info=True;User ID=sa;Password=BytMig123!;Trust Server Certificate=True")
            .EnableSensitiveDataLogging()
            .Options;

        var context = new DataContext(options);
        await ClearInstructorsAndRolesAsync(context);

        var repo = new InstructorRoleRepository(context);
        var role = InstructorRole.Create(1, "Admin");

        var result = await repo.CreateAsync(role, ct);

        Assert.True(result);
    }

    private static async Task ClearInstructorsAndRolesAsync(DataContext context)
    {
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Instructors;");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM InstructorRoles;");
    }
}
