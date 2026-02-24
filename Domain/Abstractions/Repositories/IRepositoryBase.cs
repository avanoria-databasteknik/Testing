namespace Domain.Abstractions.Repositories;

public interface IRepositoryBase<TModel, TId>
{
    Task<bool> CreateAsync(TModel model, CancellationToken ct = default);
    Task<bool> UpdateAsync(TId id, TModel model, CancellationToken ct = default);
    Task<bool> DeleteByIdAsync(TId id, CancellationToken ct = default);

    Task<TModel?> GetByIdAsync(TId id, CancellationToken ct = default);
    Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct = default);
}
