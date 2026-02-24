using Domain.Abstractions.Repositories.Instructors;
using Infrastructure.Persistence.EfCore.Contexts;
using Infrastructure.Persistence.EfCore.Repositories.Instructors;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions;

public static class InfrastructureServiceRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        if (env.IsDevelopment())
        {
            services.AddSingleton(_ =>
            {
                var connectionString = configuration.GetConnectionString("DevelopmentDB")
                    ?? "Data Source=:memory:;Cache=Shared";

                var conn = new SqliteConnection(connectionString);
                conn.Open();

                Console.WriteLine("Development");
                Console.WriteLine($"{connectionString}");

                return conn;
            });

            services.AddDbContext<DataContext>((sp, options) =>
            {
                var conn = sp.GetRequiredService<SqliteConnection>();
                options.UseSqlite(conn);
            });


        }
        else
        {
            var connectionString = configuration.GetConnectionString("ProductionDB")
                ?? throw new InvalidOperationException("Missing Production DB Connection");

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            Console.WriteLine("Production");
            Console.WriteLine($"{connectionString}");
        }

        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<IInstructorRoleRepository, InstructorRoleRepository>();

        return services;
    }
}
