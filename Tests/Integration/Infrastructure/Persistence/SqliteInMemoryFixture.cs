using Infrastructure.Persistence.EfCore.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration.Infrastructure.Persistence;

public sealed class SqliteInMemoryFixture : IAsyncLifetime
{
    private SqliteConnection? _conn;
    public DbContextOptions<DataContext> Options { get; private set; } = default!;

    public async Task DisposeAsync()
    {
        if (_conn is not null)
            await _conn.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        _conn = new SqliteConnection("Data Source=:memory:;Cache=Shared");
        await _conn.OpenAsync();

        Options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlite(_conn)
            .EnableSensitiveDataLogging()
            .Options;

        await using var context = new DataContext(Options);
        await context.Database.EnsureCreatedAsync();
    }

    public DataContext CreateContext() => new(Options);
}

[CollectionDefinition(Name)]
public sealed class SqliteInMemoryCollection : ICollectionFixture<SqliteInMemoryFixture>
{
    public const string Name = "SqliteInMemory";
}
