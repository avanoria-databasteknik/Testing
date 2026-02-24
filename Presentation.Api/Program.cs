using Application.Extensions;
using Domain.Abstractions.Repositories.Instructors;
using Domain.Instructors;
using Infrastructure.Extensions;
using Infrastructure.Persistence.EfCore.Contexts;
using Presentation.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();

builder.Services.AddApplication(builder.Configuration, builder.Environment);
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    await db.Database.EnsureCreatedAsync();
}

app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapPost("/api/instructorroles", async (CreateInstructorRoleRequest request, IInstructorRoleRepository repo, CancellationToken ct) =>
{
    var role = InstructorRole.Create(request.RoleName);
    var result = await repo.CreateAsync(role, ct);
    return result ? Results.Created("/api/instructorroles/", null) : Results.BadRequest();
});

app.MapGet("/api/instructorroles", async (IInstructorRoleRepository repo, CancellationToken ct) =>
{
    var roles = await repo.GetAllAsync(ct);
    return roles == null ? Results.BadRequest() : Results.Ok(roles);
});

app.Run();

