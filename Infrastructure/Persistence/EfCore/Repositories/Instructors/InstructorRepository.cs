using Domain.Abstractions.Repositories.Instructors;
using Domain.Instructors;
using Infrastructure.Persistence.EfCore.Contexts;
using Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EfCore.Repositories.Instructors;

public sealed class InstructorRepository(DataContext context)  : RepositoryBase<Instructor, string, InstructorEntity, DataContext>(context), 
    IInstructorRepository
{
    public override async Task<Instructor?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var existing = await Set
            .AsNoTracking()
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return existing is null ? null : ToModel(existing);
    }


    public async Task<Instructor?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        var existing = await Set
            .AsNoTracking()
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email, ct);

        return existing is null ? null : ToModel(existing);
    }

    public override async Task<IReadOnlyList<Instructor>> GetAllAsync(CancellationToken ct = default)
    {
        var existing = await Set
            .AsNoTracking()
            .Include(x => x.Role)
            .ToListAsync(ct);

        return [.. existing.Select(ToModel)];
    }

    public override async Task<bool> CreateAsync(Instructor model, CancellationToken ct = default)
    {
        if (model is null)
            throw new InvalidOperationException("Domain model is required.");

        var entity = ToEntity(model);
        
        var date = DateTime.UtcNow;
        entity.CreatedAt = date;
        entity.ModifiedAt = date;

        await Set.AddAsync(entity, ct);
        var saved = await Context.SaveChangesAsync(ct);

        return saved > 0;
    }

    protected override InstructorEntity ToEntity(Instructor model)
    {
        var entity = new InstructorEntity
        {
            Id  = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email.Value,
            RoleId = model.Role.Id,
            ModifiedAt = DateTime.UtcNow,
        };

        return entity;
    }

    protected override Instructor ToModel(InstructorEntity entity)
    {
        var role = InstructorRole.Create(entity.Role.Id, entity.Role.RoleName);

        var model = Instructor.Create
            (
                entity.Id,
                entity.FirstName,
                entity.LastName,
                entity.Email,
                role
            );

        return model;
    }
}
