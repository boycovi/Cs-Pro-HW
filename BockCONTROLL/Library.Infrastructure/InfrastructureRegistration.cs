using Library.Core.Common;
using Library.Core.Domain.Authors.Common;
using Library.Infrastructure.Core.Common;
using Library.Infrastructure.Core.Domain.Authors.Common;
using Library.Infrastructure.Processing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Library.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // mediatr
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthorsRepository, AuthorsRepository>();

        // processing
        services.AddScoped<IEnumerationIgnorer, EnumerationIgnorer>();
    }
}
