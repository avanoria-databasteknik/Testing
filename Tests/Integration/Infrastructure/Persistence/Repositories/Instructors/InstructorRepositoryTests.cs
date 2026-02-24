using Domain.Instructors;
using Infrastructure.Persistence.EfCore.Contexts;
using Infrastructure.Persistence.EfCore.Repositories.Instructors;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration.Infrastructure.Persistence.Repositories.Instructors;

[Collection(SqliteInMemoryCollection.Name)]
public sealed class InstructorRepositoryTests(SqliteInMemoryFixture fixture)
{
    [Fact]
    public async Task CreateAsync_ShouldCreateInstructor_ReturnTrue()
    {
        CancellationToken ct = CancellationToken.None;
        await using var context = fixture.CreateContext();

        await AddDefaultRolesAsync(context);

        var repo = new InstructorRepository(context);
        var role = InstructorRole.Create(1, "Admin");
        var instructor = Instructor.Create
            (
                Guid.NewGuid().ToString(),
                "Hans",
                "Mattin-Lassei",
                "hans@domain.com",
                role
            );


        var result = await repo.CreateAsync(instructor, ct);

        Assert.True(result);
    }

    private static async Task AddDefaultRolesAsync(DataContext context)
    {
        await context.Database.ExecuteSqlRawAsync("INSERT INTO InstructorRoles VALUES (1, 'Admin');");
    }

}
