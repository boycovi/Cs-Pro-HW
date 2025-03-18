using University.Core.Common;
using University.Persistence.UniversityDb;

namespace University.Infrastructure.Core.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly UniversityDbContext _universityDbContext;

    public UnitOfWork(UniversityDbContext universityDbContext)
    {
        _universityDbContext = universityDbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _universityDbContext.SaveChangesAsync(cancellationToken);
    }
}