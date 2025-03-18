using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using University.Persistence.UniversityDb;

namespace University.Persistence;

public static class PersistenceRegistration
{
    private const string ConnectionString = "UniversityDb";

    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionString);
        services.AddDbContext<UniversityDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}