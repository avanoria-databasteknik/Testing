using Domain.Abstractions.Repositories.Instructors;
using Domain.Instructors;
using Infrastructure.Persistence.EfCore.Contexts;
using Infrastructure.Persistence.EfCore.Entities;

namespace Infrastructure.Persistence.EfCore.Repositories.Instructors;

public sealed class InstructorRoleRepository(DataContext context) : RepositoryBase<InstructorRole, int, InstructorRoleEntity, DataContext>(context), IInstructorRoleRepository
{
    protected override InstructorRoleEntity ToEntity(InstructorRole model)
    {
        var entity = new InstructorRoleEntity
        {
            RoleName = model.RoleName,
        };

        return entity;
    }

    protected override InstructorRole ToModel(InstructorRoleEntity entity)
    {
        var model = InstructorRole.Create(entity.Id, entity.RoleName);
        return model;
    }
}
