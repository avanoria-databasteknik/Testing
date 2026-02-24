using Domain.Instructors;

namespace Domain.Abstractions.Repositories.Instructors;

public interface IInstructorRepository : IRepositoryBase<Instructor, string>
{
    Task<Instructor?> GetByEmailAsync(string email, CancellationToken ct = default);
}
