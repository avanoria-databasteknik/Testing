using Azure.Core;
using Domain.Instructors;
using Infrastructure.Persistence.EfCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Api.Models;
using System.Net;
using System.Net.Http.Json;

namespace Tests.E2E.Instructors;

public sealed class InstructorRolesEndpointTests(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Post_ShouldCreateRole_Return201Created()
    {
        await ClearInstructorRolesAsync();

        var request = new { roleName = "Admin" };

        var response = await _client.PostAsJsonAsync("/api/instructorroles", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task GetAll_ShouldReturnStatusCode200WithListOfRoles()
    {
        await ClearInstructorRolesAsync();
        await AddDefaultInstructorRolesAsync();

        var response = await _client.GetAsync("/api/instructorroles");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var roles = await response.Content.ReadFromJsonAsync<List<InstructorRoleResult>>();
        Assert.NotNull(roles);
        Assert.Single(roles);
    }

    private async Task ClearInstructorRolesAsync()
    {
        using var scope = factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        await context.Database.ExecuteSqlRawAsync("DELETE FROM InstructorRoles");
    }

    private async Task AddDefaultInstructorRolesAsync()
    {
        using var scope = factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        await context.Database.ExecuteSqlRawAsync("INSERT INTO InstructorRoles VALUES (1, 'Admin')");
    }
}


