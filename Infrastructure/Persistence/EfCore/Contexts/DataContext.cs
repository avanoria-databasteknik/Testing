using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EfCore.Contexts;

public sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
}
