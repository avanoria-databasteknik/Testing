using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.EfCore.Repositories;

public abstract class RepositoryBase<TModel, TId, TEntity, TContext>(TContext context) : IRepositoryBase<TModel, TId> where TEntity : class where TContext : DbContext
{
    protected readonly TContext Context = context;
    protected DbSet<TEntity> Set => Context.Set<TEntity>();

    protected abstract TModel ToModel(TEntity entity);
    protected abstract TEntity ToEntity(TModel model);

    public virtual async Task<bool> CreateAsync(TModel model, CancellationToken ct = default)
    {
        if (model is null)
            throw new InvalidOperationException("Domain model is required.");

        var entity = ToEntity(model);
        await Set.AddAsync(entity, ct);
        var saved = await Context.SaveChangesAsync(ct);
        
        return saved > 0;
    }

    public virtual async Task<bool> UpdateAsync(TId id, TModel model, CancellationToken ct = default)
    {
        var existing = await Set.FindAsync([id], ct);
        if (existing is null)
            return false;

        var updated = ToEntity(model);

        Context.Entry(existing).CurrentValues.SetValues(updated);
        var saved = await Context.SaveChangesAsync(ct);
        return saved > 0;
    }

    public virtual async Task<bool> DeleteByIdAsync(TId id, CancellationToken ct = default)
    {
        var existing = await Set.FindAsync([id], ct);
        if (existing is null)
            return false;

        Set.Remove(existing);
        var deleted = await Context.SaveChangesAsync(ct);
        return deleted > 0;
    }

    public virtual async Task<TModel?> GetByIdAsync(TId id, CancellationToken ct = default)
    {
        var existing = await Set.FindAsync([id], ct);
        return existing is null ? default : ToModel(existing);
    }

    public virtual async Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct = default)
    {
        var existing = await Set.AsNoTracking().ToListAsync(ct);
        return [.. existing.Select(ToModel)];
    }
}
